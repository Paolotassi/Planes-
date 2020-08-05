# AutoPlanes
--OVERVIEW--
An arduino autopilot for a model airplane
This system needs 2 Arduinos and a PC.
With the chosen radio modules, the distance between the PC and the plane needs to be less than 1 km to recieve and transmitt data.
Despite that, after the preflight procedures the plane can fly completely by itself at greater distances, although the operator won't recieve telemetry.

--FLIGHT PLAN--
The flight plan file consists of a series of waypoints, modeTypes, and PID constants.
The Waypoints are designated by latitude, longitude, altitude, and mode.
-latitude, longitude: represented by whole deg. and then decimals (e.g.: 45.5°= 45°30'). They can have up to 7 decimals.
-altitude: represented in meters.
-mode: specifies what the waypoint is: 1 is direct route, 2 is take off, 3 is landing, 4 is barrel roll, and we may add more. 
The modetypes indicate the series of maneuvres that the plane must do before reaching the waypoint.
The PID constants regulates the plane control surfaces.


--SETUP--
An Arduino Mega is mounted on the plane, connected to the motor, servos, GPS, accelerometer, gyroscope, magnetic compass, and radio modules. We call it "Autopilot".
An Arduino nano (or uno) is connected to the PC and to a radio module. We call it "Radio".
Last, we have a PC program made with LabView, called "ControlCenter".

--ROLES--
-Autopilot: Drives the plane through the waypoints, and sends telemetry data to Radio.
-Radio: Relays data between Autopilot and ControlCenter.
-ControlCenter: displays telemetry data and sends the flight plan to Radio.

--TELEMETRY--
Flight Data from the plane is sent in one single package, in a string organized like this:
-'f'
-number of the waypoint the plane is pointing to 
-speed (m/s)
-altitude (m)
-Roll angle
-Roll correction
-Pitch
-Pitch correction
-latitude
-longitude
-power
-'l'
With 'f' and 'l' are two characters placed there to verify data integrity.
Every number is sent as an integer, so ControlCenter needs to scale down the latitude and longitude 
(the others are already integers to begin with)

--INSTRUCTIONS--
First, wait until the plane moves twice the control surfaces, with 1 sec. between the two.
Then, you can start to follow the instructions displayed by ControlCenter.

--TAKE OFF--
The plane must be pointed in the right direction, as it won't change course until the specified altitude is reached.

--LAND--
The landing function just stops the engine and glides down with a constant pitch angle. So, previous waypoints should be used 
to alogn the plane to the runway, and to bring it at a low altitude.

--AEROBATICS--
Evey aerobatic mode will execute a specific maneuvre BEFORE reaching the waypoint. 
So, in order to do the maneuvre in a specific point/altitude, you should specify those coordinates in the
previous waypoint.






