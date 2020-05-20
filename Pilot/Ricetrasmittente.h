

#ifndef Ricetrasmittente_h
#define Ricetrasmittente_h

#include "Arduino.h"
#include <RF22.h>



class Ricetrasmittente {

  public:

    //Return true if everything was successful
    bool SetUp();

    //Recieve data from the ground
    void Recieve(uint8_t *data);

    //Send data to the ground
    void Send(uint8_t *data, uint8_t lenght);

    //waits until there's a message for us
    void WaitMessage();

};

#endif
