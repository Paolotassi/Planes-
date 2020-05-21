//serial buffer size: 64 bytes

#include <SPI.h>
#include <RF22.h>
#include <string.h>

#define TX_POWER 15

// Singleton instance of the radio
RF22 rf22;

int maxTime = 50000; //se una risposta non viene ricevuta entro maxTime ms, si considera errore

//variabili per collegamento con PC
String Data = "";      //stringa con comandi o dati
String Answer = "";    //stringa con telemetria (o altre risposte)

void setup()
{
  Serial.begin(115200); //setup porta seriale

  rf22.init(); //setup ricetrasmittente

  rf22.setTxPower(TX_POWER);
}

void loop()
{
  //while (!Serial.available()) {}
  while (Serial.available())  //ricevi una richiesta
  {
    Data = Serial.readString();
  }
  //Serial.flush();

  if (Data != "") //inoltra la richiesta
  {
    //int len = Data.length()+10;
    int len = 50;
    uint8_t data[len];

    Data.getBytes(data, len);  //trasforma la stringa Data in array di bytes

    rf22.send(data, len);
    rf22.waitPacketSent(500);
    Data = "";

    if (rf22.waitAvailableTimeout(maxTime)) //ricevi una risposta
    {
      uint8_t buf_size = 50; //verifica a quanto impostare questo
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
  }
}

void telemetria()
{
  while (1) //continua in eterno
  {
    String Info;
    //stai sempre in ascolto rispetto a Pilot, e aggiorna in contunuo
    //Answer con i dati ricevuti.
    //Se richiesto, inoltrali a ControlCenter

    if (rf22.waitAvailableTimeout(maxTime)) //ricevi una risposta
    {
      uint8_t buf_size = 50; //verifica a quanto impostare questo
      uint8_t answer[buf_size];
      rf22.recv(answer, &buf_size);
      Answer = (char*)answer;
      if (Answer.length() != 49)
        Answer = "";
    }

    //inoltra la risposta
    while (Serial.available())
    {
      Info = Serial.readString();
    }
    if (Info == "info" && Answer != "")
    {
      Serial.print(Answer);
      Answer = "";
    }


  }

  /*if (rf22.waitAvailableTimeout(maxTime)) //ricevi una risposta
    {
    uint8_t buf_size = 50; //verifica a quanto impostare questo
    uint8_t answer[buf_size];
    rf22.recv(answer, &buf_size);
    Answer = (char*)answer;
    //inoltra la risposta

    Serial.flush();
    Serial.print(Answer);
    Answer = "";
    }*/
}
