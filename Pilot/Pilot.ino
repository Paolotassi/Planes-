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
#define Pi 3.1415926 //costante pi greco
#define DELAY 500  //intervallo MINIMO in millisecondi tra 2 cicli



/*------ COSTANTI PID -------*/
#define  P_B       1 //cost. proporzionale del beccheggio
#define I_B       1 //cost. integrale del beccheggio
#define P_R       1 //cost. proporzionale del rollio 
#define I_R       1 //cost. integrale del rollio
#define P_I       1 //cost. proporzionale dell'imbardata
#define I_I       1 //cost. integrale dell'imbardata
#define SAMPLING_FREQ 100 //frequenza di campionamento

/*------ STRUTTURE ---------*/

typedef struct {

  long lat;   //latitudine
  long lng;   //longitudine
  int alm;    //altezza livello del mare
  byte mode;   //come arrivare al waypoint
  bool reached; //TRUE se sei arrivato al waypont

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
int posX = 90; //posizione servo. N.B.: prima di inviarle, va sottratto 90
int posY = 90;
int deX, deY; //movimento richiesto ai servo


//Dati sulla posizione
int speed; //velocità in metri al secondo
int dir, dist;  //direzione effettiva dell'aereo e distanza percorsa (calcolate con la diff. tra 2 rilevamenti gps consecutivi)

//Dati sulla rotta
int distWp; //distanza effettiva dal waypoint
int XWp, YWp, dirWp; //rotta diretta per il waypoint
int errDist = 2; //distanza in metri dal waypoint al di sotto della quale il wp si considera raggiunto
int deltaDir; //distanza angolare tra la rotta del momento e quella da seguire per arrivare al waypoint

//Piano di volo
Waypoint wp[N];   //array che contiene il percorso stabilito
int nWp;  //numero effettivo di waypoints


//x è per DEBUG, va tolta sia qui che in readGPS!!
int x = 0;


/*------ PROGRAMMA -------*/

//RF22 rf22;
Ricetrasmittente Radio;
Sensori sensori;
//TinyGPSPlus gps;


void setup() {

  //setup Servo
  servoX.attach(8);
  servoY.attach(9);
  servoX.write(90);
  servoY.write(90);
  Serial.begin(115200);
  Serial1.begin(9600);
  Wire.begin();

  for (int i = 0; i < N; i++) { //azzera tutti i reached e i mode
      wp[i].reached = 0;
      wp[i].mode = 0;
  }

    //setup sensori
  sensori.SetUp();

  //fa un "saluto" con tutte le superfici di controllo: avvisa che si può iniziare a trasmettere
  servoX.write(0);
  servoY.write(0);
  delay (1000);
  servoX.write(90);
  servoY.write(90);
  /*//setup giroscopio-accelerometro
  setUpMPU();

  //setup bussola
  setUpCompass();

  

  GPSReady(); //aspetta che il GPS sia connesso con almeno un tot di satelliti*/

  //setup ricetrasmittente
  // if(!rf22.init())
  if (!Radio.SetUp())
    Serial.println("failed");
  

  //Status = 0 -> richiesta connessione
  //Status = 1 -> ricevi numero waypoints
  while (Status < 2) {
    Recieve(m);
    delay(500);
    Send(Answer);
  }

  //Status = 2 -> ricevi i waypoints
  if (Status == 2) {
    for (int m = 0; m < nWp ; m++) {
      Recieve(m);
      delay(500);
      Send(Answer);
    }
  }


  sensori.readGPS(&pos); //ricevi il primo pacco di dati


  //INVIA POSIZIONE INIZIALE
  if (Status == 3) {
    Recieve(m);
    delay(500);
    Send(Answer);
  }

  //ASPETTA AUTORIZZAZIONE AL VOLO
  if (Status == 4) {
    Recieve(m);
    delay(500);
    Send(Answer);
  }

  //CONFERMA INIZIO VOLO. PASSA AL LOOP
  if (Status != 5)
    Send( "f000000000000000000000000000000000000000000errorl" );

  m = 0; //azzera quest'indice, che deve essere usato anche nel loop
}

void loop() {
  //Per come è strutturato, è molto probabile che il ciclo duri più di DELAY
  //Quindi per il calcolo della velocità si usa la differenza tra loopTime e millis()

  sensori.readGPS(&pos); //ricevi la posizione

  
  Serial.println("1");
  SpeedDirection(); //trova velocità e direzione; decide se passare al waypoint successivo
  /*Serial.println("2");
  readAndProcessAccelData();  //dati dall'accelerometro
  Serial.println("3");
  readCompassData(); //dati dalla bussola*/
  Serial.println("4");
  //Invia telemetria
  createTelemetry();
  Send(Answer);
  Serial.println("5");
  newRoute(); //trova e segui la nuova rotta. N.B.: c'è un loop per cui non si passa alla prossima funzione finchè non si è in rotta
  loopTime = millis();
  Serial.println("6");
  sensori.readGPS(&pos); //ricevi la posizione
  Serial.println("7");
  PresentToPast(&pos, &posPast);  //salva la posizione pos in posPast
  Serial.println("8");
  //Invia telemetria
  createTelemetry();
  Send(Answer);
  Serial.println("9");
  while ( millis() - loopTime < DELAY  ) //aspetta per ottenere un intervallo DELAY tra 2 rilevamenti GPS
    PID(); //mantiene una rotta stabile, secondo i parametri stabiliti da newRoute
  Serial.println("10");
}//loop





/*------ FUNZIONI DI TRASMISSIONE -------*/

void createTelemetry() {
  int x;
  Relevant = "";
  Answer = 'f';

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

  Answer = Answer + 'l';


}

void Send( String Answer ) {

  Serial.println("invio... ");
  uint8_t answer[buf_size];
  Answer.getBytes(answer, buf_size);  //trasforma la stringa Answer in array di bytes
  /*rf22.send(answer, sizeof(answer));
    rf22.waitPacketSent(500);*/
  Radio.Send(answer, sizeof(answer));
  Serial.print("inviato: ");
  Answer = (char*)answer;
  Serial.println(Answer);
  Answer = "";
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
  Serial.flush();
  Serial.println(Data);
  Serial.print("status: ");
  Serial.println(Status);
  if (Data == "ready" && Status == 0) { //Controllo della connessione
    Answer = "yes";
    Status = 1;


  } else if (Status == 1) { //Ricevi il numero di Waypoints
    Answer = Data;
    Status = 2;
    nWp = Answer.toInt();

  } else if (Status == 2) { //Ricevi i Waypoints
    Answer = (i + 1);

    Relevant = Data.substring(1, 11); //11 bytes
    wp[i].lat = Relevant.toInt();
    if (Data[0] == '-')
      wp[i].lat = - wp[i].lat;

    Relevant = Data.substring(12, 22); //11 bytes
    wp[i].lng = Relevant.toInt();
    if (Data[11] == '-')
      wp[i].lng = - wp[i].lng;

    Relevant = Data.substring(22, 25); //3 bytes
    wp[i].alm = Relevant.toInt();

    Relevant = Data.substring(25, 26); //1 byte
    wp[i].mode = Relevant.toInt();

    if (i == nWp - 1) //aggiorna Status dopo aver caricato tutti i waypoints
      Status = 3;




  } else if (Data == "info" && Status == 3) { //invia la prima stringa di dati



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
    Answer = Answer + 'l';


    Status = 4;

  } else if (Data == "start" && Status == 4 ) {
    Answer = "yes";
    Status = 5;

  } else
    Answer = "nope";


}

/*------ FUNZIONI DI LOOP -------*/


void SpeedDirection() {

  dist = distanza(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, m);//distanza dal rilevamento precedente
  speed = dist / (millis() - loopTime);
  if (dist > 0)
    dir = direzione(posPast.lat, posPast.lng, pos.lat, pos.lng, GtoMLat, GtoMLong, dist, m);//angolo col nord di un vettore posPast->pos

  distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m); //calcolo della distanza dal waypoint
  if (distWp < errDist) { //se è stato raggiunto il waypoint, passa a quello successivo
    wp[m].reached = 1;
    m++;
    distWp = distanza(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, m);
  }//if


}

int distanza(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, int m) { //misura la distanza tra 2 punti
  int deltaLat, deltaLong;

  deltaLat = ((lat2 - lat1) / 10000000) * GtoMLat;
  deltaLong = (( lng2 - lng1 ) / 10000000) * GtoMLong * ( cos(lng2) + cos(lng1) ) / 2;

  return sqrt( deltaLat * deltaLat + deltaLong * deltaLong );
}//distanza

int direzione(long lat1, long lng1, long lat2, long lng2, long GtoMLat, long GtoMLong, int dist, int m) { //calcola la rotta
  int deltaLat, deltaLong;
  double arsin;

  deltaLat = ( lat2 - lat1 ) * GtoMLat;
  deltaLong = ( lng2 - lng1 ) * GtoMLong * cos(lng2);
  arsin = asin(deltaLat / dist) * 180 / Pi;
  if (arsin >= 0)
    return (acos(deltaLong / dist) * 180 / Pi);
  else
    return (-acos(deltaLong / dist) * 180 / Pi);

}//distanza

void newRoute() {

  if (wp[m].mode == 1) {    //rotta diretta per il waypoint

    XWp = asin((wp[m].alm - pos.alm) / dist) * 180 / Pi; //calcolo della rotta diretta verso il waypoint
    YWp = 0;              //non ci dovrebbe essere rollio
    dirWp = direzione(pos.lat, pos.lng, wp[m].lat, wp[m].lng, GtoMLat, GtoMLong, distWp, m); //calcola la nuova rotta
    deltaDir = dirWp - dir;

    PID();


  } else if (wp[m].mode == 2) {
  } else if (wp[m].mode == 3) {
  }


}

void PID() {

  //FUNZIONAMENTO:
  //L'aereo non avrà un timone, quindi si possono controllare soltanto rollio (y) e beccheggio (x)
  //Per cambiare rotta, l'aereo si inclina su un lato (quindi il rollio è anche proporzionale
  //all'angolo tra rotta attuale e rotta desiderata)
  //Questo abbassa il muso dell'aereo, che verrà quindi bilanciato dal controllore del beccheggio

  pidVariables(); //trova X e dirCompass
  deDir = dirCompass + deltaDir; //la rotta a cui dirCompass dovrà essere uguale a fine ciclo

  i_accumulator_B = 0;
  i_accumulator_R = 0;
  i_accumulator_I = 0;

  while ( (deDir - dirCompass) < -3 || (deDir - dirCompass) > 3 || (datiGrezzi.angX - XWp) < -3 || (datiGrezzi.angX - XWp) > 3 || (datiGrezzi.angY - YWp) < -3 || (datiGrezzi.angY - YWp) > 3) { //finchè la rotta è entro limiti accettabili
    time = millis();

    sensori.ReadSensors(&datiGrezzi);
    

    //Invia telemetria
    createTelemetry();
    Send(Answer);

    pidVariables(); //trova X e dirCompass


    //PID per beccheggio
    error = XWp - datiGrezzi.angX;
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
    error = error - datiGrezzi.angY; //il rollio aumenta con l'aumentare dell'errore dell'imbardata
    product = P_B * error;
    integral = I_B * error / SAMPLING_FREQ;
    i_accumulator_R += integral;
    pid_Y = product + i_accumulator_R;


    //posizione SERVO
    deX = pid_X; //a 90° il servo è in posizione neutrale
    if ( deX < -45 )
      deX = -45;
    else if ( deX > 45 )
      deX = 45;

    deY =  pid_Y + pid_Z;
    if ( deY < -45 )
      deY = -45;
    else if ( deY > 45 )
      deY = 45;

    posX = 90 + deX;
    posY = 90 + deY;

    Serial.print("PosX= ");
    Serial.print(posX);
    Serial.print("   PosY= ");
    Serial.println(posY);

    servoX.write(posX);
    servoY.write(posY);

    while ( (millis() - time) < (1000 / SAMPLING_FREQ) ) { //aspetta per far sì che il ciclo duri il giusto
      //non so se è un tempo sufficiente, ma in caso affermativo
      //Invia telemetria
      createTelemetry();
      Send(Answer);
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
