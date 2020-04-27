//serial buffer size: 64 bytes

#include <SPI.h>
#include <RF22.h>
#include <string.h>
// Singleton instance of the radio
RF22 rf22;


int maxTime=5000;//se una risposta non viene ricevuta entro maxTime ms, si considera errore
byte inVolo = 0; // TRUE solo se è richiesto l'inizio del volo

//variabili per collegamento con PC
String Data;      //stringa con comandi o dati
String Answer;    //stringa con telemetria (o altre risposte)



void setup() {
  //setup porta seriale
   Serial.begin(9600);
   Data="";
   

  //setup ricetrasmittente
  rf22.init();
   
  
  
}
 
void loop() {
  //ricevi una richiesta
  while(!Serial.available()){}
  while (Serial.available()){
    Data=Serial.readString();
   }
  Serial.flush();
  
  if( Data == "start")  //verifica se il volo può iniziare
    inVolo = 1;
  
  //inoltra la richiesta
  int len = sizeof(Data);
   uint8_t data[len];
   uint8_t buf_size=50; //verifica a quanto impostare questo
   uint8_t answer[buf_size];
   
   Data.getBytes(data, len);  //trasforma la strinfa Data in array di bytes
   
  rf22.send(data, sizeof(data));
    rf22.waitPacketSent();
  
  //ricevi una risposta
  if (rf22.waitAvailableTimeout(maxTime)){ 
      // Should be a message for us now   
      rf22.recv(answer, &buf_size);
    Answer= (char*)answer;
    }
  else 
  Answer = "nope";

//inoltra la risposta
Serial.flush();
  Serial.print(Answer);
  
  if(inVolo == 1) 
    telemetria(); //riceve sempre i dati da Pilot, e li inoltra quando richiesto da ControlCenter
  
  
 }

 
 void telemetria(){
   while(1){  //continua in eterno 
     //stai sempre in ascolto rispetto a Pilot, e aggiorna in contunuo
     //Answer con i dati ricevuti.
     //Se richiesto, inoltrali a ControlCenter
     
   }
 }
