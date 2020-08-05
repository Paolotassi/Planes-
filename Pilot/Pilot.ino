//l'aereo si sviluppa in lunghezza lungo l'asse y
//l'asse x va da sinistra a destra
//l'asse z va dal basso verso l'alto

/*---DEBUG---*/
//Baud rate monitor seriale = 115200

//CONNESSIONI


#include "Ricetrasmittente.h"
#include "Sensori.h"

#include "Arduino.h"
#include <stdio.h>
#include <Wire.h>
#include <SoftwareSerial.h>
//#include <TinyGPS++.h>
#include <string.h>
#include <math.h>
#include <Servo.h>
#include <SPI.h>
//#include <RF22.h>

#define N 20    //numero massimo di waypoints
#define DUNO 10    //dimensione 1 di modeType
#define DDUE 10 //dimensione 2 di modeType
#define DTRE 5    //dimensione 3 di modeType
#define Pi 3.1415926 //costante pi greco
#define DELAY 500  //intervallo MINIMO in millisecondi tra 2 cicli
#define errDist 2 //distanza in metri dal waypoint al di sotto della quale il wp si considera raggiunto
#define errAng 2 //distanza in gradi dalla rotta indicata al di sotto del queale loa rotta si considera raggiunta


/*------ STRUTTURE ---------*/

typedef struct {

  long lat;   //latitudine
  long lng;   //longitudine
  int alm;    //altezza livello del mare
  byte mode;   //come arrivare al waypoint
  byte reached; //1 se sei arrivato al waypont. Vale 2 sui waypoints non utilizzati

} Waypoint;





/*------ VARIABILI ---------*/

int m = 0;  //indice utilizzato dal loop per scorrere i waypoints
unsigned long time = 0; //prende il tempo quando richiesto
unsigned long loopTime = 0; //prende il tempo del ciclo di loop

//RICETRASMETTITORE
String Data;      //stringa con il comando o dato ricevuto
String Answer;    //stringa con telemetria (o altre risposte)
String Relevant;  //sottostringa di Data, contiene solo i caratteri utili
uint8_t buf_size = 50; //verifica a quanto impostare questo
byte Status = 0;  //individua a che punto del Setup siamo. Permette di interpretare correttamente le trasmissioni

//GPS
Position pos;   //posizione attuale
Position posPast; //posizione al ciclo precedente
//byte nSat = 0; //numero di satelliti connessi al GPS
long GtoMLat = 111136; //conversione gradi->metri lungo un meridiano (1 deg= 111136 m)
long GtoMLong = 111322; //conversione gradi->metri lungo un parallelo (1 deg= 111322 m all'equatore)
//NB: va moltiplicato per cos(latitudine)per latitudini diverse da 0

//SENSORI
Dati datiGrezzi;

//ACCELEROMETRO
long accelX, accelY, accelZ;
float gForceX, gForceY, gForceZ;
//int angX = 0, angY = 0; //rotazione attorno all'asse X, Y

//BUSSOLA
//int compX, compY, compZ;  //misura del campo magnetico

/*------ COSTANTI PID -------*/
float P_B;        //cost. proporzionale del beccheggio
float I_B;        //cost. integrale del beccheggio
float D_B;        //cost. derivata del becheggio
float P_R;        //cost. proporzionale del rollio 
float I_R;        //cost. integrale del rollio
float D_R;        //cost. derivata del rollio
float P_I;        //cost. proporzionale dell'imbardata
float I_I;        //cost. integrale dell'imbardata
float D_I;        //cost. derivata dell'imbardata
int SAMPLING_FREQ;  //frequenza di campionamento

//PID
int pid_X, pid_Y, pid_Z;  //valore dei PID
float error;
float product, integral, derivative, i_accumulator_B, i_accumulator_I, i_accumulator_R;
int deDir; //differenza tra l'angolo di un asse e l'angolo desiderato
float dirCompass; //direzione secondo la bussola
float newX, newY;


//SERVO
Servo servoX;
Servo servoY;
int posX = 90; //posizione servo. N.B.: prima di inviarle, va sottratto 90
int posY = 90;
int prevposX = 0; //posizione vecchia servo.
int prevposY = 0;
int deX, deY; //movimento richiesto ai servo


//Dati sulla posizione
int speed; //velocità in metri al secondo
float dir, dist;  //direzione effettiva dell'aereo e distanza percorsa (calcolate con la diff. tra 2 rilevamenti gps consecutivi)
int power = 0; //% potenza del motore

//Dati sulla rotta
float distWp; //distanza effettiva dal waypoint
float XWp, YWp, dirWp; //rotta diretta per il waypoint
int deltaDir; //distanza angolare tra la rotta del momento e quella da seguire per arrivare al waypoint
/*modeType:
UNO: numero del mode
DUE: fase del mode
TRE: angoli/tipo di condizione
valore: valore della condizione
*/
int modeType[DUNO][DDUE][DTRE]; //contiene i mode definiti dall'utilizzatore
int d2=0;  //contatorie utilizzato per scorrere modeType


//Piano di volo
Waypoint wp[N];   //array che contiene il percorso stabilito
int nWp;  //numero effettivo di waypoints


//mnb è per DEBUG, va tolta sia qui che in createTel!!
int mnb = 0;


/*------ PROGRAMMA -------*/

//RF22 rf22;
Ricetrasmittente Radio;
Sensori sensori;
//TinyGPSPlus gps;


void setup() {

  //setup Servo
  servoX.attach(10);
  servoY.attach(11);
  //servoPos (prevposX, posX);
  //servoPos (prevposY, posY);
  servoX.write(posX);
  servoY.write(posY);
  prevposX = posX;
  prevposY = posY;
  Serial.begin(115200);//serve solo finchè facciamo opere di debug
  
  //Serial.println("1");
  for (int i = 0; i < N; i++) { //azzera tutti i reached e i mode
      wp[i].reached = 2; 
      wp[i].mode = 0;
  }
  //Serial.println("2");
  for (int i = 0; i < DUNO; i++) { //inizializza modeType
    for (int j = 0; j < DDUE; j++) { 
      for (int k = 0; k < DTRE; k++) { 
        modeType[i][j][k]=0;
        
      
      }
    }
  }

//Serial.println("3");

    //setup sensori
  sensori.SetUp();
  //Serial.println("4");

  //fa un "saluto" con tutte le superfici di controllo: avvisa che si può iniziare a trasmettere
  /*servoPos (prevposX, 0);
  servoPos (prevposY, 0);
  prevposX = 0;
  prevposY = 0;
  
  delay (1000);
  servoPos (prevposX, 90);
  servoPos (prevposY, 90);
  prevposX = 90;
  prevposY = 90;*/
  servoX.write(5);
  servoY.write(5);
  delay(1000);
  servoX.write(90);
  servoY.write(90);


  //setup ricetrasmittente
  // if(!rf22.init())
  if (!Radio.SetUp())
    Serial.println("failed");
  

  //Status = 0 -> richiesta connessione
  while (Status ==0) {
    Recieve(m);
    //delay(500);
    Send(Answer);
  }

  //Status = 1 -> ricevi i waypoints
  m=0;
  while (Status == 1) {
    Recieve(m);
    //delay(500);
    Send(Answer);
   m++; 
  }

  //Ricevi i modeType
  while (Status == 2) {
    Recieve(m);
    //delay(500);
    Send(Answer);
   m++; 
  }

  //Ricevi le costanti PID
  while (Status == 3) {
    Recieve(m);
    //delay(500);
    Send(Answer);
   m++; 
  }
  
  sensori.readGPS(&pos); //ricevi il primo pacco di dati


  //INVIA POSIZIONE INIZIALE
  if (Status == 4) {
    Recieve(m);
    //delay(500);
    Send(Answer);
  }

  //ASPETTA AUTORIZZAZIONE AL VOLO
  if (Status == 5) {
    Recieve(m);
    //delay(500);
    Send(Answer);
  }

  //CONFERMA INIZIO VOLO. PASSA AL LOOP
  if (Status != 6)
    Send( "f000000000000000000000000000000000000000000errorl" );

  m = 0; //azzera quest'indice, che deve essere usato anche nel loop


}

void loop() {
  //Per come è strutturato, è molto probabile che il ciclo duri più di DELAY
  //Quindi per il calcolo della velocità si usa la differenza tra loopTime e millis()
  if(wp[m].reached == 2){
    Send("f000000000000000000000000000000000000000000000errorl");
    //spegni i motori
    delay(1000000);
  }
  sensori.readGPS(&pos); //ricevi la posizione

  
  Serial.println("1");
  SpeedDirection(); //trova velocità e direzione; decide se passare al waypoint successivo
  Serial.println("2");
  /*readAndProcessAccelData();  //dati dall'accelerometro
  Serial.println("3");
  readCompassData(); //dati dalla bussola*/
  //Serial.println("4");
  //Invia telemetria
  createTelemetry();
  Send(Answer);
  Serial.println("3");
  newRoute(); //trova e segui la nuova rotta. N.B.: c'è un loop per cui non si passa alla prossima funzione finchè non si è in rotta
  Serial.println("4");
  loopTime = millis();
  //Serial.println("6");
  sensori.readGPS(&pos); //ricevi la posizione
  Serial.println("5");
  PresentToPast(&pos, &posPast);  //salva la posizione pos in posPast
  Serial.println("6");
  //Invia telemetria
  createTelemetry();
  Send(Answer);
  //Serial.println("7");
  while ( millis() - loopTime < DELAY  ) //aspetta per ottenere un intervallo DELAY tra 2 rilevamenti GPS
    PID(); //mantiene una rotta stabile, secondo i parametri stabiliti da newRoute
  //Serial.println("10");
}//loop





/*------ FUNZIONI DI TRASMISSIONE -------*/

void createTelemetry() {
  int x;
  Relevant = "";
  Answer = 'f';

  /*//DEBUG
  pos.lng+=mnb;
  pos.lat+=mnb;
  mnb+=10;
  //FINE DEBUG*/
  
  //Waypoint
  if (m < 10)
    Relevant = "00" + String(m);
  else if (m < 100)
    Relevant = '0' + String(m);
  else
    Relevant = String(m);
  Answer = Answer + Relevant;

  //Velocità
  if (speed < 10)
    Relevant = "00" + String(speed);
  else if (speed < 100)
    Relevant = '0' + String(speed);
  else
    Relevant = String(speed);
  Answer = Answer + Relevant;

  //Altitudine
  if (pos.alm < 10)
    Relevant = "00" + String(pos.alm);
  else if (pos.alm < 100)
    Relevant = '0' + String(pos.alm);
  else
    Relevant = String(pos.alm);
  Answer = Answer + Relevant;

  //Rollio
  if (datiGrezzi.angY >= 0) {
    if (datiGrezzi.angY < 10)
      Relevant = "000" + String(datiGrezzi.angY);
    else if (datiGrezzi.angY < 100)
      Relevant = "00" + String(datiGrezzi.angY);
    else
      Relevant = '0' + String(datiGrezzi.angY);
    Answer = Answer + Relevant;
  } else {
    if ( (-datiGrezzi.angY) < 10)
      Relevant = "00" + String((-datiGrezzi.angY));
    else if ( (-datiGrezzi.angY) < 100)
      Relevant = '0' + String((-datiGrezzi.angY));
    else
      Relevant = String((-datiGrezzi.angY));
    Answer = Answer + '-' + Relevant;
  }

  //Azione Servo rollio
  x = posY * 4 - 90; //così va da + a - 180, si visualizza meglio su labview
  if (x >= 0) {
    if (x < 10)
      Relevant = "000" + String(x);
    else if (x < 100)
      Relevant = "00" + String(x);
    else
      Relevant = '0' + String(x);
    Answer = Answer + Relevant;
  } else {
    if ( (-x) < 10)
      Relevant = "00" + String((-x));
    else if ( (-x) < 100)
      Relevant = '0' + String((-x));
    else
      Relevant = String((-x));
    Answer = Answer + '-' + Relevant;
  }

  //Beccheggio
  if (datiGrezzi.angX >= 0) {
    if (datiGrezzi.angX < 10)
      Relevant = "000" + String(datiGrezzi.angX);
    else if (datiGrezzi.angX < 100)
      Relevant = "00" + String(datiGrezzi.angX);
    else
      Relevant = '0' + String(datiGrezzi.angX);
    Answer = Answer + Relevant;
  } else {
    if ( (-datiGrezzi.angX) < 10)
      Relevant = "00" + String((-datiGrezzi.angX));
    else if ( (-datiGrezzi.angX) < 100)
      Relevant = '0' + String((-datiGrezzi.angX));
    else
      Relevant = String((-datiGrezzi.angX));
    Answer = Answer + '-' + Relevant;
  }

  //Azione Servo beccheggio
  x = posX * 4 - 90; //così va da + a - 180, si visualizza meglio su labview
  if (x >= 0) {
    if (x < 10)
      Relevant = "000" + String(x);
    else if (x < 100)
      Relevant = "00" + String(x);
    else
      Relevant = '0' + String(x);
    Answer = Answer + Relevant;
  } else {
    if ( (-x) < 10)
      Relevant = "00" + String((-x));
    else if ( (-x) < 100)
      Relevant = '0' + String((-x));
    else
      Relevant = String((-x));
    Answer = Answer + '-' + Relevant;
  }

  //Latitudine
  if (pos.lat > 0 ) {
    if (pos.lat < 1 * 10000000)
      Relevant = "0000" + String(pos.lat);
    else if (pos.lat < 10 * 10000000)
      Relevant = "000" + String(pos.lat);
    else if (pos.lat < 100 * 10000000)
      Relevant = "00" + String(pos.lat);
    else
      Relevant = '0' + String(pos.lat);

  } else if (pos.lat < 0 ) {
    if ((-pos.lat) < 1 * 10000000)
      Relevant = "-000" + String((-pos.lat));
    else if (-pos.lat < 10 * 10000000)
      Relevant = "-00" + String((-pos.lat));
    else if (-pos.lat < 100 * 10000000)
      Relevant = "-0" + String((-pos.lat));
    else
      Relevant = '-' + String((-pos.lat));

  }
  Answer = Answer + Relevant;

  //Longitudine
  if (pos.lng > 0 ) {
    if (pos.lng < 1 * 10000000)
      Relevant = "0000" + String(pos.lng);
    else if (pos.lng < 10 * 10000000)
      Relevant = "000" + String(pos.lng);
    else if (pos.lng < 100 * 10000000)
      Relevant = "00" + String(pos.lng);
    else
      Relevant = '0'  + String(pos.lng);

  } else if (pos.lng < 0 ) {
    if ((-pos.lng) < 1 * 10000000)
      Relevant = "-000" + String((-pos.lng));
    else if (-pos.lng < 10 * 10000000)
      Relevant = "-00" + String((-pos.lng));
    else if (-pos.lng < 100 * 10000000)
      Relevant = "-0" + String((-pos.lng));
    else
      Relevant = '-' + String((-pos.lng));

  }
  Answer = Answer + Relevant;
  
  //Power
  Relevant = "000";
  if(power == 0)
    Relevant = "000";
  else if(power < 10)
    Relevant = "00" + String(power);
  else if(power < 100)
    Relevant = "0" + String(power);
  else
    Relevant = String(power);
  Answer = Answer + 'l';


}

void Send( String Answer ) {

  //Serial.println("invio... ");
  uint8_t answer[buf_size];
  Answer.getBytes(answer, buf_size);  //trasforma la stringa Answer in array di bytes
  /*rf22.send(answer, sizeof(answer));
    rf22.waitPacketSent(500);*/
  Radio.Send(answer, sizeof(answer));
  Serial.print("inviato: ");
  Answer = (char*)answer;
  Serial.println(Answer);
  Answer = "";
  /*Serial.print("# satelliti: ");
  Serial.println(pos.nSat);*/
}

void Recieve(int i) {
  Data = "";
  uint8_t data[buf_size];
  Serial.print("ricevo: ");
  /*rf22.waitAvailable();
    // Should be a message for us now
    rf22.recv(data, &buf_size);*/
  Radio.WaitMessage();
  Radio.Recieve(data);

  Data = (char*)data;
    Serial.println(Data);
  Serial.flush();
  /*Serial.println(Data);
  Serial.print("status: ");
  Serial.println(Status);*/
  if (Data == "ready" && Status == 0) { //Controllo della connessione
    Answer = "yes";
    Status = 1;


  } else if (Status == 1) { //Ricevi i Waypoints
    Answer = (i);
    wp[i].reached = 0;
    Relevant = Data.substring(1, 11); //11 bytes
    wp[i].lat = Relevant.toInt();
    if (Data[0] == '-')
      wp[i].lat = - wp[i].lat;

    Relevant = Data.substring(12, 22); //11 bytes
    wp[i].lng = Relevant.toInt();
    if (Data[11] == '-')
      wp[i].lng = - wp[i].lng;

    Relevant = Data.substring(23, 26); //4 bytes
    wp[i].alm = Relevant.toInt();

    Relevant = Data.substring(27, 28); //2 bytes
    wp[i].mode = Relevant.toInt();

    if (Data == "modeType") //aggiorna Status dopo aver caricato tutti i waypoints
      Status = 2;

  } else if (Status == 2) {//ricevi i modeType
    if (Data == "pid")
      Status = 3;
    else {
      int x, y, z;
      Relevant = Data.substring(1, 2); //2 bytes
      x = Relevant.toInt();
      Relevant = Data.substring(3, 4); //2 bytes
      y = Relevant.toInt();
      Relevant = Data.substring(5, 8); //4 bytes
      modeType[x][y][0] = Relevant.toInt();
      Relevant = Data.substring(9, 112); //4 bytes
      modeType[x][y][1] = Relevant.toInt();
      Relevant = Data.substring(13, 16); //4 bytes
      modeType[x][y][2] = Relevant.toInt();
      Relevant = Data.substring(17, 18); //2 bytes
      modeType[x][y][3] = Relevant.toInt();
      Relevant = Data.substring(19, 22); //4 bytes
      modeType[x][y][4] = Relevant.toInt();
      
    }

    Answer = String(i);
    
   } else if (Status == 3) {//ricevi le costanti pid
     Answer = String(i);
     if (Data == "end")
        Status = 4;
    else{
      float x;
      Relevant = Data.substring(1, 4); //4 bytes
      x = Relevant.toInt();
      P_B = x/100;

      Relevant = Data.substring(5, 8); //4 bytes
      x = Relevant.toInt();
      I_B = x/100;
      
      Relevant = Data.substring(9, 12); //4 bytes
      x = Relevant.toInt();
      D_B = x/100;

      Relevant = Data.substring(13,16); //4 bytes
      x = Relevant.toInt();
      P_R = x/100;

      Relevant = Data.substring(17, 20); //4 bytes
      x = Relevant.toInt();
      I_R = x/100;

      Relevant = Data.substring(21, 24); //4 bytes
      x = Relevant.toInt();
      D_R = x/100;

      Relevant = Data.substring(25, 28); //4 bytes
      x = Relevant.toInt();
      P_I = x/100;
      
      Relevant = Data.substring(29, 32); //4 bytes
      x = Relevant.toInt();
      I_I = x/100;
      
      Relevant = Data.substring(33, 36); //4 bytes
      x = Relevant.toInt();
      D_I = x/100;
      
      Relevant = Data.substring(37, 40); //4 bytes
      x = Relevant.toInt();
      SAMPLING_FREQ = x;
      
    }
    
  } else if (Data == "info" && Status == 4) { //invia la prima stringa di dati



    Answer = "";
    Answer = Answer + 'f';
    Answer = Answer + "000000";
    //Answer = Answer + "000";


    if (pos.alm < 10)
      Relevant = "00" + String(pos.alm);
    else if (pos.alm < 100)
      Relevant = '0' + String(pos.alm);
    else
      Relevant = String(pos.alm);

    Answer = Answer + Relevant;

    Answer = Answer + "0000000000000000";



    if (pos.lat > 0 ) { //sistema la stringa per la latitudine
      if (pos.lat < 1 * 10000000)
        Relevant = "0000" + String(pos.lat);
      else if (pos.lat < 10 * 10000000)
        Relevant = "000" + String(pos.lat);
      else if (pos.lat < 100 * 10000000)
        Relevant = "00" + String(pos.lat);
      else
        Relevant = '0' + String(pos.lat);

    } else if (pos.lat < 0 ) {
      if ((-pos.lat) < 1 * 10000000)
        Relevant = "-000" + String(-pos.lat);
      else if (-pos.lat < 10 * 10000000)
        Relevant = "-00" + String(-pos.lat);
      else if (-pos.lat < 100 * 10000000)
        Relevant = "-0" + String(-pos.lat);
      else
        Relevant = '-' + String(-pos.lat);

    }
    Answer = Answer + Relevant;


    if (pos.lng > 0 ) { //sistema la stringa per la longitudine
      if (pos.lng < 1 * 10000000)
        Relevant = "0000" + String(pos.lng);
      else if (pos.lng < 10 * 10000000)
        Relevant = "000" + String(pos.lng);
      else if (pos.lng < 100 * 10000000)
        Relevant = "00" + String(pos.lng);
      else
        Relevant = '0'  + String(pos.lng);

    } else if (pos.lng < 0 ) {
      if ((-pos.lng) < 1 * 10000000)
        Relevant = "-000" + String(-pos.lng);
      else if (-pos.lng < 10 * 10000000)
        Relevant = "-00" + String(-pos.lng);
      else if (-pos.lng < 100 * 10000000)
        Relevant = "-0" + String(-pos.lng);
      else
        Relevant = '-' + String(-pos.lng);

    }
    Answer = Answer + Relevant;
    Answer = Answer + "000"; //aggiungi "power"
    Answer = Answer + 'l';


    Status = 5;

  } else if (Data == "start" && Status == 5 ) {
    Answer = "yes";
    Status = 6;

  } else
    Answer = "nope";


}

/*------ FUNZIONI DI LOOP -------*/


void SpeedDirection() {

  dist = distanza(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, m);//distanza dal rilevamento precedente
  dist = sqrt( dist*dist + (pos.alm - posPast.alm)*(pos.alm - posPast.alm)  );
  speed = dist / (millis() - loopTime);
  if (dist > 0)
    dir = direzione(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, dist, m);//angolo col nord di un vettore posPast->pos

  distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m); //calcolo della distanza dal waypoint
  distWp = sqrt( distWp*distWp + (pos.alm - posPast.alm)*(pos.alm - posPast.alm)  );
  
  if (distWp < errDist) { //se è stato raggiunto il waypoint, passa a quello successivo
    wp[m].reached = 1; 
    m++;
    d2=0;
    distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m);
    distWp = sqrt( distWp*distWp + (pos.alm - wp[m].alm)*(pos.alm - wp[m].alm)  );
  }//if


}

int distanza(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, int m) { //misura la distanza tra 2 punti
  float deltaLat, deltaLong;

  deltaLat = ((float)(lat2 - lat1) / 10000000) * GtoMLat;
  deltaLong = ((float)( lng2 - lng1 ) / 10000000) * GtoMLong * ( cos((lng2/ 10000000)*Pi/180 ) + cos((lng1/ 10000000)*Pi/180 )) / 2;

  return sqrt( deltaLat * deltaLat + deltaLong * deltaLong );
}//distanza

float direzione(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, float dist, int m) { //calcola la rotta
  float deltaLat, deltaLong;
  float arsin;

  deltaLat = (float)(( lat2 - lat1 ) * GtoMLat)/ 10000000;
  deltaLong = (float)(((( lng2 - lng1 ) * GtoMLong)/ 10000000) * (float)cos(lng2/ 10000000));

  /*Serial.print("deltaLat= ");
  Serial.print(deltaLat);
  Serial.print("  deltaLong= ");
  Serial.println(deltaLong);*/
  
  arsin = asin(deltaLat / dist) * 180 / Pi;
  if (arsin >= 0)
    return (acos(deltaLong / dist) * 180 / Pi);
  else
    return (-acos(deltaLong / dist) * 180 / Pi);

}//distanza

void newRoute() {
  sensori.ReadSensors(&datiGrezzi); //prendi i dati dai sensori
  pidVariables(); //trova dirCompass
  float startDir = dirCompass; //trova la direzione bussola di questo istante
  
  //Impostazione dei valori per il PID
  if( modeType[wp[m].mode][d2][3] == 0 ){

    XWp = asin((wp[m].alm - pos.alm) / distWp) * 180 / Pi; //calcolo della rotta diretta verso il waypoint
    YWp = modeType[wp[m].mode][d2][1];
    
    dirWp = direzione(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, distWp, m); //calcola la nuova rotta    
    deltaDir = dirWp - dir;
    if(deltaDir > 180)
      deltaDir = -360 + deltaDir;
    else  if(deltaDir < -180) 
      deltaDir = 360 + deltaDir;
    
    
  } else {
    
    XWp = modeType[wp[m].mode][d2][0];
    YWp = modeType[wp[m].mode][d2][1];
    deltaDir = modeType[wp[m].mode][d2][2];
    
  }
  
  PID();

  //verifica che le condizioni siano state raggiunte
  sensori.readGPS(&pos); //ricevi la posizione
  sensori.ReadSensors(&datiGrezzi); //prendi i dati dai sensori
  
  if (modeType[wp[m].mode][d2][3] == 1){//se la condizione è raggiungere un'altezza
    if(pos.alm >=(modeType[wp[m].mode][d2][4] - errDist) && pos.alm <= (modeType[wp[m].mode][d2][4] + errDist) ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 2){//se la condizione è una distanza dal waypoint (compresa l'altezza)
    float lontananza = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m);
    lontananza = sqrt( lontananza*lontananza + (pos.alm - wp[m].alm)*(pos.alm - wp[m].alm)  );
    if(lontananza >=(modeType[wp[m].mode][d2][4] - errDist) && lontananza <= (modeType[wp[m].mode][d2][4] + errDist) ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 3){//se la condizione è una distanza dal waypoint (senza contare l'altezza)
    float lontananza = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m);
    if(lontananza >=(modeType[wp[m].mode][d2][4] - errDist) && lontananza <= (modeType[wp[m].mode][d2][4] + errDist) ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 4){//se la condizione è un angolo di rollio
    if( (YWp - datiGrezzi.angY >= -errAng) && (YWp - datiGrezzi.angY <= errAng) ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 5){//se la condizione è un angolo di beccheggio
    if( (XWp - datiGrezzi.angX >= -errAng) && (XWp - datiGrezzi.angX <= errAng) ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 6){//se la condizione è Delta angolo di imbardata
    pidVariables(); //trova dirCompass
    if( (dirCompass - startDir) <= deltaDir + errAng && (dirCompass - startDir) >= deltaDir - errAng ) 
      d2++;
  }else if (modeType[wp[m].mode][d2][3] == 7){//se la condizione è regolare la potenza del motore
      //trova il modo di regolarla
      power = modeType[wp[m].mode][d2][4]; //è la % di potenza, va da 0 a 100
  }

}


void PID() {

  //FUNZIONAMENTO:
  //L'aereo non avrà un timone, quindi si possono controllare soltanto rollio (y) e beccheggio (x)
  //Per cambiare rotta, l'aereo si inclina su un lato (quindi il rollio è anche proporzionale
  //all'angolo tra rotta attuale e rotta desiderata)
  //Questo abbassa il muso dell'aereo, che verrà quindi bilanciato dal controllore del beccheggio
  
  int i=0;
  
  sensori.ReadSensors(&datiGrezzi); //prendi i dati dai sensori
  sensori.readGPS(&pos); //ricevi la posizione

  pidVariables(); //trova X e dirCompass
  deDir = dirCompass + deltaDir; //la rotta a cui dirCompass dovrà essere uguale a fine ciclo

  Serial.println("FUORI DAL PID");
/*
Serial.print("XWp= ");
    Serial.print(XWp);
    Serial.print("  distWp= ");
    Serial.print(distWp);
    Serial.print("  dirWp= ");
    Serial.print(dirWp);
    Serial.print("  dirCompass= ");
    Serial.print(dirCompass);
    
    Serial.print("  angX= ");
    Serial.print(datiGrezzi.angX);
    Serial.print("  angY= ");
    Serial.print(datiGrezzi.angY);
    
    Serial.print("  PosX= ");
    Serial.print(posX-90);
    Serial.print("  PosY= ");
    Serial.println(posY-90);*/
    
  i_accumulator_B = 0;
  i_accumulator_R = 0;
  i_accumulator_I = 0;

  while ( (deDir - dirCompass) < -3 || (deDir - dirCompass) > 3 || (datiGrezzi.angX - XWp) < -3 || (datiGrezzi.angX - XWp) > 3 || (datiGrezzi.angY - YWp) < -3 || (datiGrezzi.angY - YWp) > 3) { //finchè la rotta è entro limiti accettabili
    time = millis();

    i++;
    sensori.ReadSensors(&datiGrezzi);
    sensori.readGPS(&pos); //ricevi la posizione

    //Invia telemetria
    createTelemetry();
    Send(Answer);

    pidVariables(); //trova dirCompass

    //VERIFICA BENE IL DERIVATIVE, E COME METTERLO IN IMBARDATA

    //PID per beccheggio
    error = XWp - datiGrezzi.angX;
    product = P_B * error;
    derivative = D_B * datiGrezzi.gyroX;
    integral = I_B * error / SAMPLING_FREQ;
    i_accumulator_B += integral;
    pid_X = product + derivative + i_accumulator_B;
    
    if(modeType[wp[m].mode][d2][3]!=4){//se la condizione non è l'angolo di rollio  
      //PID per imbardata
      error = deDir - dirCompass;
      product = P_I * error;
      integral = I_I * error / SAMPLING_FREQ;
      i_accumulator_I += integral;
      pid_Z = product + i_accumulator_I;
 
      //pid_Z è limitato dal massimo rollio che ha senso dare all'aereo. Ora è +-60 gradi, ma si può ovviamente cambiare
      if (pid_Z > 60)
        pid_Z = 60;
      else if (pid_Z < -60)
        pid_Z = -60;   
   
    }else{//se la condizione è il rollio, si ignora la deviazione dalla rotta
      pid_Z = 0;
    }
    
    //PID per rollio
    error = (pid_Z + YWp) - datiGrezzi.angY; //il rollio aumenta con l'aumentare dell'errore dell'imbardata
    product = P_R * error;
    derivative = D_R * datiGrezzi.gyroY;
    integral = I_R * error / SAMPLING_FREQ;
    i_accumulator_R += integral;
    pid_Y = product + derivative + i_accumulator_R;


    //posizione SERVO
    deX = pid_X; //a 90° il servo è in posizione neutrale
    if ( deX < -45 )
      deX = -45;
    else if ( deX > 45 )
      deX = 45;

    deY =  pid_Y;
    if ( deY < -45 )
      deY = -45;
    else if ( deY > 45 )
      deY = 45;
      
    //prevposX = posX;
    //prevposY = posY;
    posX = 90 + deX;
    posY = 90 + deY;


    /*Serial.print("XWp= ");
    Serial.print(XWp);
    Serial.print("  dirWp= ");
    Serial.print(dirWp);
    Serial.print("  erroreRotta= ");
    if(-deDir + dirCompass > 180)
      Serial.print(-360 -deDir + dirCompass);
    else  if(-deDir + dirCompass < -180) 
      Serial.print(360 -deDir + dirCompass);
    else
       Serial.print(-deDir + dirCompass);
       
    Serial.print("  dirCompass= ");
    Serial.print(dirCompass);
    Serial.print("  angX= ");
    Serial.print(datiGrezzi.angX);
    Serial.print("  angY= ");
    Serial.print(datiGrezzi.angY);
    
    Serial.print("  deX= ");
    Serial.print(deX);
    Serial.print("  deY= ");
    Serial.print(deY);
    Serial.print("  pid_Z= ");
    Serial.println(pid_Z);*/
    //servoPos (prevposX, posX);
    //servoPos (prevposY, posY);
    servoX.write(posX);
    servoY.write(posY);

    //azzera gli integrali dei controllori dopo "i" cicli
    if(i>=100){
      i=0;
      i_accumulator_B = 0;
      i_accumulator_R = 0;
      i_accumulator_I = 0;
    }
      
    while ( (millis() - time) < (1000 / SAMPLING_FREQ) ) { //aspetta per far sì che il ciclo duri il giusto
      //non so se è un tempo sufficiente, ma in caso affermativo
     /* //Invia telemetria
      createTelemetry();
      Send(Answer);*/
    }
  }

}

void pidVariables() {

  newX = datiGrezzi.compX * cos(-datiGrezzi.angX * Pi / 180) + datiGrezzi.compY * sin(-datiGrezzi.angY * Pi / 180) * sin(datiGrezzi.angX * Pi / 180) - datiGrezzi.compZ * cos(-datiGrezzi.angY * Pi / 180) * sin(datiGrezzi.angX * Pi / 180) ;
  newY = datiGrezzi.compY * cos(-datiGrezzi.angY * Pi / 180) + datiGrezzi.compZ * sin(-datiGrezzi.angY * Pi / 180) ;
  dirCompass = atan2( newY, newX ) * 180 / Pi;

}

void PresentToPast(Position *pos, Position *posPast) { //salva la posizione pos in posPast

  posPast->lat  = pos->lat ;
  posPast->lng  = pos->lng ;
  posPast->alm  = pos->alm ;
  posPast->nSat = pos->nSat;

}//PresentToPast
/*
void servoPos(int prev, int p){ 
  for(int i=prev; i<p; i+=5){
    servoX.write(i);
    delay(5);
  }
  servoX.write(p);
}*/
