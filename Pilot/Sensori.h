


#ifndef Sensori_h
#define Sensori_h

#include "Arduino.h"
#include <Wire.h>
#include <TinyGPS++.h>
#include <math.h>


typedef struct {

  
  int angX = 0, angY = 0; //rotazione attorno all'asse X, Y
  int gyroX, gyroY, gyroZ;  //accelerazione angolare attorno agli assi
  int compX, compY, compZ;  //misura del campo magnetico

} Dati;

typedef struct {

  long lat;   //latitudine
  long lng;   //longitudine
  int alm;    //altezza livello del mare
  byte nSat; //numero di satelliti connessi al GPS

} Position;

class Sensori{
	
  public:
    void SetUp();
    void ReadSensors(Dati *dati);
    void readGPS(Position *pos);
    
  private:
    void setUpMPU();
    void setUpCompass();
    void GPSReady();
    
    void readAndProcessAccelData(Dati *dati);
    void readAndProcessGyroData(Dati *dati);
    void readCompassData(Dati *dati);
};

#endif
