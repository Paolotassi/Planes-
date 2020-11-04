
#include "Arduino.h"
#include "Ricetrasmittente.h"
#include <RF22.h>


uint8_t buffSize = 55;

RF22 rf22;

bool Ricetrasmittente::SetUp() {
  rf22.init();
  rf22.setTxPower(RF22_TXPOW_14DBM);
  rf22.setModemConfig(9);
  return true;
  /*if (!rf22.init()){
    rf22.setTxPower(RF22_TXPOW_14DBM);
    rf22.setModemConfig(9);
    return HIGH;
  } else {
    return LOW;
  }
  //return (rf22.init());*/
}

void Ricetrasmittente::Recieve(uint8_t *data) {
  //uint8_t buffSize = 50;

  rf22.recv(data, &buffSize);

}

void Ricetrasmittente::Send(uint8_t *data, uint8_t lenght) {
  rf22.send(data, lenght);
  rf22.waitPacketSent();

}

void Ricetrasmittente::WaitMessage() {
  rf22.waitAvailable();
}
