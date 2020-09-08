


#include "Arduino.h"
#include "Sensori.h"
#include <Adafruit_BMP280.h>
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

/*------ COSTANTI BAROMETRO -------*/

#define BMP_SCK  (3)
#define BMP_MISO (4)
#define BMP_MOSI (5)
#define BMP_CS   (6)


Dati dati;
TinyGPSPlus gps;
Adafruit_BMP280 bmp; // I2C

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
  Wire.write(0x0B); // Set the Register
  Wire.write(0x01); // Tell the HMC5883 to Continuously Measure
  Wire.endTransmission();
 
  Wire.beginTransmission(0x0D); //start talking
  Wire.write(0x09); // Set the Register
  Wire.write(Mode_Continuous|ODR_200Hz|RNG_8G|OSR_512); // Tell the HMC5883 to Continuously Measure
  Wire.endTransmission();
}

void Sensori::GPSReady() {
   int nSat=0; 
   
    while (nSat<6){//connesso con almeno 6 satelliti
      while (Serial1.available() > 0){
          gps.encode(Serial1.read());
          nSat=gps.satellites.value();

        }
    }
}

void Sensori::setUpBarometer(){
  Serial.println(F("BMP280 test"));

  if (!bmp.begin()) {
    Serial.println(F("Could not find a valid BMP280 sensor, check wiring!"));
    while (1);
  }else 
    Serial.println(F("Barometer working fine"));

  /* Default settings from datasheet. */
  bmp.setSampling(Adafruit_BMP280::MODE_NORMAL,     /* Operating Mode. */
                  Adafruit_BMP280::SAMPLING_NONE,     /* Temp. oversampling */
                  Adafruit_BMP280::SAMPLING_X16,    /* Pressure oversampling */
                  Adafruit_BMP280::FILTER_OFF,      /* Filtering. */
                  Adafruit_BMP280::STANDBY_MS_1); /* Standby time. */
}

void Sensori::SetUp(){
  Serial1.begin(9600);
  Wire.begin();
  setUpMPU();
  setUpCompass();
  setUpBarometer();
  GPSReady();
}

//DATA

void Sensori::readGPS(Position *pos) {
  if(Serial1.available() <= 0)
    Serial.println("no signal");
  while (Serial1.available() > 0){
      gps.encode(Serial1.read());
      pos->lat=gps.location.lat()*10000000;
      pos->lng=gps.location.lng()*10000000;
      pos->alm=bmp.readAltitude(1013.25);
      pos->nSat=gps.satellites.value();

    }
    
  /*
  pos->lat = -1 * 10000000;
  pos->lng = 2 * 10000000;
  pos->alm = 3;
  pos->nSat = 5;*/

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

void Sensori::readAndProcessGyroData(Dati *dati) {
  Wire.beginTransmission(0b1101000);                          // Start the communication by using address of MPU 
  Wire.write(0x43);                                           // Access the starting register of gyro readings
  Wire.endTransmission();
  Wire.requestFrom(0b1101000,6);                              // Request for 6 bytes from gyro registers (43 - 48)
  while(Wire.available() < 6);                                // Wait untill all 6 bytes are available
  dati->gyroX = Wire.read()<<8|Wire.read();                  // Store first two bytes into gyroX
  dati->gyroY = Wire.read()<<8|Wire.read();                  // Store next two bytes into gyroY
  dati->gyroZ = Wire.read()<<8|Wire.read();                  //Store last two bytes into gyroZ
  
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

void Sensori::readBarometer(Dati *dati){
  dati->alm=bmp.readAltitude(1013.25);
}

void Sensori::ReadSensors(Dati *dati){
  readAndProcessAccelData(dati);
  readAndProcessGyroData(dati);
  readCompassData(dati);
  readBarometer(dati);
}
