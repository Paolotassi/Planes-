//serial buffer size: 64 bytes
//#include <Servo.h> //serve per debugging


#include <SPI.h>
#include <RF22.h>
#include <string.h>

//input per la funzione di telecomando
#define BUTTON 4
#define X A1
#define Y A2
#define M A3  //motore

// Singleton instance of the radio
RF22 rf22;

int maxTime = 200; //se una risposta non viene ricevuta entro maxTime ms, si considera errore

//variabili per collegamento con PC
String Data = "";      //stringa con comandi o dati
String Answer = "";    //stringa con telemetria (o altre risposte)


int x, y, m; //valori da inviare se in modalità telecomando

void setup()
{
  pinMode(BUTTON, INPUT_PULLUP);
  pinMode(X, INPUT);
  pinMode(Y, INPUT);
  pinMode(M, INPUT);

  Serial.begin(115200); //setup porta seriale

  rf22.init(); //setup ricetrasmittente

  rf22.setTxPower(RF22_TXPOW_14DBM);
  rf22.setModemConfig(9);

  //rf22.send("trying", 60);
  //servo.attach(3); //debug
  //servo.write(0);
}

void loop()
{
  //while (!Serial.available()) {}
  if (Serial.available())  //ricevi una richiesta
  {
    Data = Serial.readString();
  }
  //Serial.flush();


  if (Data != "" && Data != "reset") //inoltra la richiesta
  {
    //int len = Data.length()+10;
    int len = 52;
    uint8_t data[len];

    //if (Data=="ready")
    // servo.write(45);

    Data.getBytes(data, len);  //trasforma la stringa Data in array di bytes

    rf22.send(data, len);
    rf22.waitPacketSent();



    if (rf22.waitAvailableTimeout(maxTime)) //ricevi una risposta
    {
      uint8_t buf_size = 55; //verifica a quanto impostare questo
      uint8_t answer[buf_size];
      rf22.recv(answer, &buf_size);
      Answer = (char*)answer;
      //inoltra la risposta

      Serial.flush();
      Serial.print(Answer);
      Answer = "";

    }
    else
    {
      Serial.flush();
      Serial.print("nope");
      Answer = "";
    }
    if (Data == "start") //verifica se il volo può iniziare
    {
      telemetria(); //riceve sempre i dati da Pilot, e li inoltra quando richiesto da ControlCenter
    }
    Data = "";
  }
}

void telemetria()
{
  byte x = 1;
  while (x == 1) //continua fino al reset
  {
    telecomando();
    //stai sempre in ascolto rispetto a Pilot, e aggiorna in contunuo
    //Answer con i dati ricevuti.
    //Se richiesto, inoltrali a ControlCenter

    if (rf22.waitAvailableTimeout(maxTime)) //ricevi una risposta
    {
      uint8_t buf_size = 55; //verifica a quanto impostare questo
      uint8_t answer[buf_size];
      rf22.recv(answer, &buf_size);
      Answer = (char*)answer;
      //if (Answer.length() != 55)
      //Answer = "";


    }

    Serial.print(Answer);
    Answer = "";
  }
}

void telecomando() {

  if (digitalRead(BUTTON) == LOW) {

    while (digitalRead(BUTTON) == LOW) {
      uint8_t data[] = "ii";
      rf22.send(data, sizeof(data));
      rf22.waitPacketSent();
    }
    
    while (digitalRead(BUTTON) == HIGH) {
      x = (analogRead(X) / 6) + 5;
      y = (analogRead(Y) / 6) + 5;
      m = (analogRead(M) / 6) + 5;

      Serial.print("SENDING:  X = ");
      Serial.print(x);
      Serial.print("   Y = ");
      Serial.print(y);
      Serial.print("   M = ");
      Serial.println(m);

      prepData();

      int len = Data.length() + 1;
      uint8_t data[len];

      Data.getBytes(data, len);  //trasforma la stringa Data in array di bytes

      Serial.print("   Data = ");
      Serial.println(Data);

      rf22.send(data, len);
      rf22.waitPacketSent();


    }

    while (digitalRead(BUTTON) == LOW) {
      uint8_t data[] = "ff";
      rf22.send(data, sizeof(data));
      rf22.waitPacketSent();
    }
  }

}

void prepData() {
  String Relevant;
  Data = "";

  if (x < 10)
    Relevant = "00" + String(x);
  else if (x < 100)
    Relevant = '0' + String(x);
  else
    Relevant = String(x);
  Data = Data + Relevant;


  if (y < 10)
    Relevant = "00" + String(y);
  else if (y < 100)
    Relevant = '0' + String(y);
  else
    Relevant = String(y);
  Data = Data + Relevant;


  if (m < 10)
    Relevant = "00" + String(m);
  else if (m < 100)
    Relevant = '0' + String(m);
  else
    Relevant = String(m);
  Data = Data + Relevant;
}
//"0000000000000000000000000000000000000000000000"
//fine
