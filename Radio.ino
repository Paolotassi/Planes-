//serial buffer size: 64 bytes

//variabili per contatori
int i=0;


//variabili per collegamento con PC
String Data;      //stringa con comandi o dati
String Answer;    //stringa con telemetria (o altre risposte complesse)
char answerSigla;   //risposta a una domanda che non richiede dati
String Status= "preflight";  //stringa con lo stato attuale

//variabili per collegamento con aereo
byte ended = 0;

//variabili navigazione
int nW = 0; //numero waypoints

void setup() {
  //setup porta seriale
   Serial.begin(9600);
   Data="";
   

  //setup ricetrasmittente

   
  
  
}
 
void loop() {
  if(Status=="preflight"){
  answerSigla='n';
  //ciclo per sincronizzare (penso sia questo il problema) la comunicazione seriale col PC
  while (answerSigla=='n'){
    while (Serial.available()){
      Data=Serial.readString();

       if (Data=="ready"){
          answerSigla='y';
       }
       /*else{
        answerSigla='n';
        }*/

    //aspetta che ci sia connessione con l'aereo
    Serial.flush();
    Serial.print(answerSigla);//affermativo, pronti
  }
  

  }
//Serial.flush();
  //delay(100);

//ricevi il numero di waypoints
while (nW==0){
   while (Serial.available()){
      Data=Serial.readString();
   }

   nW=Data.toInt();
}
//invia il numero all'aereo, e manda '0' al PC se è andato tutto bene
Serial.flush();
Serial.print(nW);


//ricevi i waypoints. il formato è lat+'a'+long+'a'+alt+'a'+mode.
//rispondi con il numero del waypoint ricevuto (da 1 a nW)
for(i=0; i<nW ; i++){
  while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
   //invia il waypoint all'aereo, e manda 'char(i+1)' al PC se è andato tutto bene
   Serial.flush();
  Serial.print(i+1);
   
  
}

//Invia la posizione iniziale al PC
while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
  if (Data=="info"){
    //ricevi dati dall'aereo
    Answer= "f00103012004006003001001000000000200000000l";//stringa con telemetria. Questo è solo un esempio
    Serial.flush();
    Serial.print(Answer);//affermativo, pronti
  }




//Partenza
answerSigla='n';
  while (answerSigla=='n'){
    while (Serial.available()){
      Data=Serial.readString();

       if (Data=="start"){
          answerSigla='y';
       }
       /*else{
        answerSigla='n';
        }*/

    //aspetta che l'aereo confermi l'avviamento
    Serial.flush();
    Serial.print(answerSigla);//affermativo, pronti
  }
  if (answerSigla=='y')
    Status="inFlight";
  }

  }



//ciclo di prova. La versione giusta ha if, ed è qui sotto commentata
int i=0;
while(i<10){

  while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
  if (Data=="info"){
    //ricevi dati dall'aereo
    Answer= "f00103012004006003001003000000000200000000l";//stringa con telemetria. Questo è solo un esempio
    Serial.flush();
    Serial.print(Answer);//affermativo, pronti
  }

  i++;
}


while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
  if (Data=="info"){
    //ricevi dati dall'aereo
    Answer= "f00103012004006003001002000000000300000000l";//stringa con telemetria. Questo è solo un esempio
    Serial.flush();
    Serial.print(Answer);//affermativo, pronti
  }

 while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
  if (Data=="info"){
    //ricevi dati dall'aereo
    Answer= "f000000000000000000000000000000000000endedl";//stringa con telemetria. Questo è solo un esempio
    Serial.flush();
    Serial.print(Answer);//affermativo, pronti
  }

/*if(Status=="inFlight"){

  while (!Serial.available()){}
   while (Serial.available()){
      Data=Serial.readString();
   }
  if (Data=="info"){
    //ricevi dati dall'aereo
    //Answer= "f00130120040060030010112";//stringa con telemetria. Questo è solo un esempio
    Serial.flush();
    Serial.print(Answer);//affermativo, pronti
  }

  
}*/



  
delay(100000);


 }
