


#include "Arduino.h"
#include "Sensori.h"
#include <Wire.h>
#include <TinyGPS++.h>
#include <math.h>

#define Pi 3.1415926 //costante pi greco

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



Dati dati;
TinyGPSPlus gps;

//SETUP

void Sensori::setUpMPU() {
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

void Sensori::setUpCompass() {
  Wire.beginTransmission(0x0D); //start talking
  Wire.write(0x02); // Set the Register
  Wire.write(0x00); // Tell the HMC5883 to Continuously Measure
  Wire.endTransmission();
}

void Sensori::GPSReady() {
   int nSat=0;
    while (nSat<5){//connesso con almeno 5 satelliti
      while (Serial1.available() > 0){
          gps.encode(Serial1.read());
          nSat=gps.satellites.value();

        }
    }
    //Wire.write(nSat);
  /*
  pos.lat = 10;
  pos.lng = 20;
  pos.alm = 30;
  */


}


void Sensori::SetUp(){
  setUpMPU();
  setUpCompass();
  GPSReady();
}

//DATA

void Sensori::readGPS(Position *pos) {
  while (Serial1.available() > 0){
      gps.encode(Serial1.read());
      pos->lat=gps.location.lat()*10000000;
      pos->lng=gps.location.lng()*10000000;
      pos->alm=gps.altitude.meters();
      pos->nSat=gps.satellites.value();

    }
  /*
  pos.lat = -1 * 10000000 + x * 1000000;
  pos.lng = 2 * 10000000 + x * 1000000;
  pos.alm = 3;
  nSat = 5;

  x++;*/
}

void Sensori::readAndProcessAccelData(Dati *dati) {
  Wire.beginTransmission(0b1101000);
  Wire.write(0x3B);
  Wire.endTransmission();
  Wire.requestFrom(0b1101000, 6);
  while (Wire.available() < 6);
  long accelX = Wire.read() << 8 | Wire.read();
  long accelY = Wire.read() << 8 | Wire.read();
  long accelZ = Wire.read() << 8 | Wire.read();
  float gForceX = accelX / 16384.0;
  float gForceY = accelY / 16384.0;
  float gForceZ = accelZ / 16384.0;

  dati->angX = atan(gForceY / sqrt(gForceZ * gForceZ + gForceX * gForceX) ) * 180 / Pi;
  dati->angY = atan(-gForceX / sqrt(gForceZ * gForceZ + gForceY * gForceY) ) * 180 / Pi;
  if ( gForceZ < 0 && gForceX < 0)
    dati->angY = 180 - dati->angY;
  else if ( gForceZ < 0 && gForceX > 0)
    dati->angY = -180 - dati->angY;
}

void Sensori::readCompassData(Dati *dati) {
  Wire.beginTransmission(0x0D);
  Wire.write(0x00);
  Wire.endTransmission();
  Wire.requestFrom(0x0D, 6);
  dati->compX = Wire.read(); //LSB  x
  dati->compX |= Wire.read() << 8; //MSB  x
  dati->compY = Wire.read(); //LSB  z
  dati->compY |= Wire.read() << 8; //MSB z
  dati->compZ = Wire.read(); //LSB y
  dati->compZ |= Wire.read() << 8; //MSB y
}

void Sensori::ReadSensors(Dati *dati){
  readAndProcessAccelData(dati);
  readCompassData(dati);
}
