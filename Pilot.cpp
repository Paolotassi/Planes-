//l'aereo si sviluppa in lunghezza lungo l'asse y
//l'asse x va da sinistra a destra  
//l'asse z va dal basso verso l'alto

#include "Arduino.h"
#include <stdio.h>
#include <Wire.h>
#include <SoftwareSerial.h>
#include <TinyGPS++.h>
#include <string.h>
#include <math.h>
#include <Servo.h>
#define N 20    //numero massimo di waypoints
#define Pi 3.1415926 //costante pi greco
#define DELAY 500	//intervallo in millisecondi tra 2 cicli

/*------ COSTANTI BUSSOLA -------*/
#define Mode_Standby    0b00000000
#define Mode_Continuous 0b00000001

#define ODR_10Hz        0b00000000
#define ODR_50Hz        0b00000100
#define ODR_100Hz       0b00001000
#define ODR_200Hz       0b00001100

#define RNG_2G          0b00000000
#define RNG_8G          0b00010000

#define OSR_512         0b00000000
#define OSR_256         0b01000000
#define OSR_128         0b10000000
#define OSR_64          0b11000000

/*------ COSTANTI PID -------*/
#define  P_B       1 //cost. proporzionale del beccheggio
#define I_B       1 //cost. integrale del beccheggio
#define P_R       1 //cost. proporzionale del rollio 
#define I_R       1 //cost. integrale del rollio
#define P_I       1 //cost. proporzionale dell'imbardata
#define I_I       1 //cost. integrale dell'imbardata
#define SAMPLING_FREQ 100 //frequenza di campionamento

/*------ STRUTTURE ---------*/

typedef struct{
	
  long lat;   //latitudine
  long lng;   //longitudine
  int alm;    //altezza livello del mare
  byte mode;   //come arrivare al waypoint
  bool reached; //TRUE se sei arrivato al waypont
  
} Waypoint;

typedef struct{
	
  long lat;   //latitudine
  long lng;   //longitudine
  int alm;    //altezza livello del mare

} Position;





/*------ VARIABILI ---------*/

int m=0;    //indice utilizzato dal loop per scorrere i waypoints
unsigned long time=0;  //prende il tempo quando richiesto


Waypoint wp[N];   //array che contiene il percorso stabilito

//GPS
Position pos;   //posizione attuale
Position posPast; //posizione al ciclo precedente
byte nSat=0;  //numero di satelliti connessi al GPS
long GtoMLat=111136;  //conversione gradi->metri lungo un meridiano (1 deg= 111136 m)
long GtoMLong=111322;   //conversione gradi->metri lungo un parallelo (1 deg= 111322 m all'equatore)
            //NB: va moltiplicato per cos(latitudine)per latitudini diverse da 0

// ACCELEROMETRO
long accelX, accelY, accelZ;
float gForceX, gForceY, gForceZ;
int angX = 0, angY = 0; //rotazione attorno all'asse X, Y

//BUSSOLA
int compX, compY, compZ;  //misura del campo magnetico

//PID
int pid_X, pid_Y, pid_Z;  //valore dei PID
int error;
float product, integral, i_accumulator_B, i_accumulator_I, i_accumulator_R;
int deDir; //differenza tra l'angolo di un asse e l'angolo desiderato
int dirCompass; //direzione secondo la bussola
float newX, newY;


//SERVO			
Servo servoX;
Servo servoY;
int posX = 90; //posizione servo
int posY = 90;
int deX, deY; //movimento richiesto ai servo			
			
			
//Dati sulla posizione			
int speed; //velocità in metri al secondo
int dir, dist;  //direzione effettiva dell'aereo e distanza percorsa (calcolate con la diff. tra 2 rilevamenti gps consecutivi)

//Dati sulla rotta
int distWp; //distanza effettiva dal waypoint
int XWp,YWp,dirWp;  //rotta diretta per il waypoint
int errDist=2;  //distanza in metri dal waypoint al di sotto della quale il wp si considera raggiunto
int deltaDir; //distanza angolare tra la rotta del momento e quella da seguire per arrivare al waypoint



/*------ PROGRAMMA -------*/

TinyGPSPlus gps;
SoftwareSerial ss(0,1); //da connettere a TX e RX rispettivamente 

void setup() {
  
	Serial.begin(9600);
	Wire.begin();
	//setup giroscopio-accelerometro
	setUpMPU(); 
  
	//setup bussola
	setUpCompass();
  
	for(int i=0; i<N; i++){  //azzera tutti i reached e i mode
	wp[i].reached=0;
	wp[i].mode=0;
	}
	//SETUP RICETRASMETTITORE
	
	//RICEZIONE PIANO DI VOLO; SALVALO IN wp
	
	//setup GPS
	Wire.begin(1);
	//Al PC: scrivi che è in attesa dei satelliti
	Wire.onRequest(GPSReady);
	//Al PC: scrivi che è pronto
	
	//ASPETTA 'OK' DAL PC PER PROCEDERE
	//Da scrivere
}

void loop(){

  
	readGPS();  //ricevi i dati dal GPS

	SpeedDirection();	//trova velocità e direzione; decide se passare al waypoint successivo
	readAndProcessAccelData();  //dati dall'accelerometro
	readCompassData(); //dati dalla bussola
	//INVIA A PC DATI SU DIREZIONE E ORIENTAMENTO
  
	newRoute();	//trova e segui la nuova rotta. N.B.: c'è un loop per cui non si passa alla prossima funzione finchè non si è in rotta 
	time= millis();
	readGPS();  //trova la posizione attuale
  
	PresentToPast(&pos, &posPast);  //salva la posizione pos in posPast
	//RICEVI EVENTUALI MODIFICHE AL PIANO DI VOLO
	//INVIA A PC DATI SU DIREZIONE E ORIENTAMENTO
	while( millis()-time < DELAY  ); //aspetta per ottenere un intervallo DELAY tra 2 rilevamenti GPS

}//loop










/*------ FUNZIONI DI LOOP -------*/

void readGPS(){
  while (ss.available() > 0){
      gps.encode(ss.read());
    
      pos.lat=gps.location.lat();
      pos.lng=gps.location.lng();
      pos.alm=gps.altitude.meters();
      nSat=gps.satellites.value();
    
    }

}

void readAndProcessAccelData() {
  Wire.beginTransmission(0b1101000); 
  Wire.write(0x3B); 
  Wire.endTransmission();
  Wire.requestFrom(0b1101000,6); 
  while(Wire.available() < 6);
  accelX = Wire.read()<<8|Wire.read(); 
  accelY = Wire.read()<<8|Wire.read(); 
  accelZ = Wire.read()<<8|Wire.read(); 
  gForceX = accelX/16384.0;
  gForceY = accelY/16384.0; 
  gForceZ = accelZ/16384.0;

  angX=atan(gForceY/sqrt(gForceZ*gForceZ + gForceX*gForceX) )*180/Pi;
  angY=atan(-gForceX/sqrt(gForceZ*gForceZ + gForceY*gForceY) )*180/Pi;
  if( gForceZ<0 && gForceX<0)
    angY=180-angY;
  else if( gForceZ<0 && gForceX>0)
    angY=-180-angY;
}

void readCompassData(){
  Wire.beginTransmission(0x0D);
  Wire.write(0x00);
  Wire.endTransmission();
  Wire.requestFrom(0x0D, 6);
  compX = Wire.read(); //LSB  x
  compX |= Wire.read() << 8; //MSB  x
  compY = Wire.read(); //LSB  z
  compY |= Wire.read() << 8; //MSB z
  compZ = Wire.read(); //LSB y
  compZ |= Wire.read() << 8; //MSB y
}

void SpeedDirection(){
	
	dist = distanza(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, m);//distanza dal rilevamento precedente
	speed = dist/DELAY;	
	if(dist>0)
		dir = direzione(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, dist, m);//angolo col nord di un vettore posPast->pos
  
	distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m); //calcolo della distanza dal waypoint
	if(distWp < errDist){ //se è stato raggiunto il waypoint, passa a quello successivo
		wp[m].reached = 1;
		m++;
		distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m);
	}//if
	
	
}

int distanza(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, int m){//misura la distanza tra 2 punti 
  int deltaLat, deltaLong;
  
  deltaLat=(lat2 - lat1)*GtoMLat;
  deltaLong=( lng2 - lng1 )*GtoMLong*( cos(lng2)+cos(lng1) )/2;

  return sqrt( deltaLat*deltaLat + deltaLong*deltaLong );
}//distanza

int direzione(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, int dist, int m){//calcola la rotta 
  int deltaLat, deltaLong;
  double arsin;
  
  deltaLat = ( lat2 - lat1 )*GtoMLat;
  deltaLong = ( lng2 - lng1 )*GtoMLong*cos(lng2);
  arsin = asin(deltaLat/dist) *180/Pi;
  if(arsin >= 0)
    return (acos(deltaLong/dist) *180/Pi);
  else
    return (-acos(deltaLong/dist) *180/Pi);
  
}//distanza

void newRoute(){
	
	if(wp[m].mode==1){        //rotta diretta per il waypoint
    //regola potenza del motore
	
    XWp=asin((wp[m].alm - pos.alm)/dist)*180/Pi; //calcolo della rotta diretta verso il waypoint
    YWp=0;                //non ci dovrebbe essere rollio
    dirWp= direzione(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, distWp, m); //calcola la nuova rotta
    deltaDir= dirWp - dir;
    
	PID();
	
	
  }else if(wp[m].mode==2){
  }else if(wp[m].mode==3){
  }
	
	
}

void PID(){
	
	pidVariables();	//trova X e dirCompass
	deDir= dirCompass + deltaDir;  //la rotta a cui dirCompass dovrà essere uguale a fine ciclo
	  
	i_accumulator_B = 0;
	i_accumulator_R = 0;
	i_accumulator_I = 0;
	  
	while( (deDir-dirCompass)<-3 || (deDir-dirCompass)>3 || (angX-XWp)<-3 || (angX-XWp)>3 ){ //finchè la rotta è entro limiti accettabili
		time = millis();
		
		readAndProcessAccelData();  //dati dall'accelerometro
		readCompassData(); //dati dalla bussola
		//INVIA A PC DATI SU ORIENTAMENTO
		
		pidVariables();	//trova X e dirCompass

	  
		//PID per beccheggio
		error = XWp - angX;
		product = P_B * error;
		integral = I_B * error / SAMPLING_FREQ;
		i_accumulator_B += integral;
		pid_X = product + i_accumulator_B;

		//PID per imbardata
		error = deDir - dirCompass;
		product = P_I * error;
		integral = I_I * error / SAMPLING_FREQ;
		i_accumulator_I += integral;
		pid_Z = product + i_accumulator_I;

		//PID per rollio
		error = error - angY; //il rollio aumenta con l'aumentare dell'errore dell'imbardata 
		product = P_B * error;
		integral = I_B * error / SAMPLING_FREQ;
		i_accumulator_R += integral;
		pid_Y = product + i_accumulator_R;
		
		
		//posizione SERVO
		deX= pid_X + pid_Z; //a 90° il servo è in posizione neutrale
		if( deX < -45 )
		  deX = -45;
		else if( deX > 45 )
		  deX = 45;
		
		deY=  pid_Y;      
		if( deY < -45 )
		  deY = -45;
		else if( deY > 45 )
		  deY = 45;
		
		posX = 90 + deX;
		posY = 90 + deY;
		while( (millis()-time) <10 ); //aspetta per far sì che il ciclo duri 10 ms
	  
	}

}

void pidVariables(){
  
  newX = compX*cos(-angX*Pi/180) + compY*sin(-angY*Pi/180)*sin(angX*Pi/180) - compZ*cos(-angY*Pi/180)*sin(angX*Pi/180) ;  
  newY = compY*cos(-angY*Pi/180) + compZ*sin(-angY*Pi/180) ;
  dirCompass = atan2( newY, newX )*180/Pi;
  
}

void PresentToPast(Position *pos, Position *posPast){//salva la posizione pos in posPast
  
  posPast->lat  = pos->lat ;
  posPast->lng  = pos->lng ;
  posPast->alm  = pos->alm ;
  
}//PresentToPast





/*------ FUNZIONI DI SETUP -------*/

void setUpMPU() {
  // power management
  Wire.beginTransmission(0b1101000);          // Start the communication by using address of MPU
  Wire.write(0x6B);                           // Access the power management register
  Wire.write(0b00000000);                     // Set sleep = 0
  Wire.endTransmission();                     // End the communication

  // configure gyro
  Wire.beginTransmission(0b1101000);
  Wire.write(0x1B);                           // Access the gyro configuration register
  Wire.write(0b00000000);
  Wire.endTransmission();

  // configure accelerometer
  Wire.beginTransmission(0b1101000);
  Wire.write(0x1C);                           // Access the accelerometer configuration register
  Wire.write(0b00000000);
  Wire.endTransmission();  
}

void setUpCompass(){
  WriteReg(0x0B,0x01);
  //Define Set/Reset period
  setMode(Mode_Continuous,ODR_200Hz,RNG_8G,OSR_512);
}

void WriteReg(byte Reg,byte val){
  Wire.beginTransmission(0x0D); //start talking
  Wire.write(Reg); // Tell the HMC5883 to Continuously Measure
  Wire.write(val); // Set the Register
  Wire.endTransmission();
}

void setMode(uint16_t mode,uint16_t odr,uint16_t rng,uint16_t osr){
  WriteReg(0x09,mode|odr|rng|osr);
}

void GPSReady(){
  while (nSat<5){
    while (ss.available() > 0){
        gps.encode(ss.read());
        nSat=gps.satellites.value();
    
      }
  }
  Wire.write(nSat);
}








