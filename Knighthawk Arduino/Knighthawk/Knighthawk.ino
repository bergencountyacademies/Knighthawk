#include <Servo.h>

// Set actuator pins, must be PWM
int actuator_front_pin = 9;
int actuator_backL_pin = 10;
int actuator_backR_pin = 11;

Servo actuator_front;
Servo actuator_backL;
Servo actuator_backR;

float frontStatus;
float backStatus;

void setup() {
  Serial.begin(9600);
  frontStatus = 0;
  backStatus = 0;
  Serial.println("Attaching Front Actuator...");
  actuator_front.attach(actuator_front_pin);
  Serial.println("Attaching Back Left Actuator...");
  actuator_backL.attach(actuator_backL_pin);
  Serial.println("Attaching Back Right Actuator...");
  actuator_backR.attach(actuator_backR_pin);
  Serial.println("All Attached.");
}

void loop() {
  while (Serial.available() != 0) {
    int val = Serial.parseInt();
    Serial.println(val);
    actuator_backL.writeMicroseconds(val);
    actuator_backR.writeMicroseconds(val);
  }
}

void initActuators() {
  Serial.println("Initializing...");
  actuator_front.writeMicroseconds(2000);
  actuator_backL.writeMicroseconds(2000);
  actuator_backR.writeMicroseconds(2000);
  updateFrontStatus(2000, 4000);
  updateBackStatus(2000, 4000);
  delay(4000);
  Serial.println("Initialized, Ready.");
}

void updateFrontStatus(int speed, int time) {
  frontStatus += ((speed-1500)/500)*(time/1000)*3;
}

void updateBackStatus(int speed, int time) {
  backStatus += ((speed-1500)/500)*(time/1000)*3;
}

// Returns true if it successfully moves up
bool tiltUp(int speed) {
  if (frontStatus < 15 && backStatus > 9) {
    actuator_front.writeMicroseconds(1639);
    actuator_backL.writeMicroseconds(1361);
    actuator_backR.writeMicroseconds(1361);
    updateFrontStatus(1639, 500);
    updateBackStatus(1361, 500);
    delay(500);
    return true;
  } else {
    actuator_front.writeMicroseconds(1500);
    actuator_front.writeMicroseconds(1500);
    actuator_front.writeMicroseconds(1500);
    updateFrontStatus(1500, 500);
    updateBackStatus(1500, 500);
    delay(500);
    return false;
  }
}

bool tiltDown(int speed) {
  if (frontStatus > 9 && backStatus < 15) {
    actuator_front.writeMicroseconds(1361);
    actuator_backL.writeMicroseconds(1639);
    actuator_backR.writeMicroseconds(1639);
    updateFrontStatus(1361, 500);
    updateBackStatus(1639, 500);
    delay(500);
    return true;
  } else {
    actuator_front.writeMicroseconds(1500);
    actuator_front.writeMicroseconds(1500);
    actuator_front.writeMicroseconds(1500);
    updateFrontStatus(1500, 500);
    updateBackStatus(1500, 500);
    delay(500);
    return false;
  }
}

void test() {
  actuator_front.writeMicroseconds(2000);
  actuator_backL.writeMicroseconds(2000);
  actuator_backR.writeMicroseconds(2000);
  Serial.println("ALL UP");
  delay(4000);
  actuator_front.writeMicroseconds(1500);
  actuator_backL.writeMicroseconds(1500);
  actuator_backR.writeMicroseconds(1500);
  Serial.println("ALL STOP");
  delay(2000);
  actuator_front.writeMicroseconds(1639);
  actuator_backL.writeMicroseconds(1361);
  actuator_backR.writeMicroseconds(1361);
  Serial.println("TILT UP");
  delay(3000);
  actuator_front.writeMicroseconds(1361);
  actuator_backL.writeMicroseconds(1639);
  actuator_backR.writeMicroseconds(1639);
  Serial.println("TILT DOWN");
  delay(6000);
  actuator_front.writeMicroseconds(1639);
  actuator_backL.writeMicroseconds(1361);
  actuator_backR.writeMicroseconds(1361);
  Serial.println("LEVEL");
  delay(3000);
  actuator_front.writeMicroseconds(1000);
  actuator_backL.writeMicroseconds(1000);
  actuator_backR.writeMicroseconds(1000);
  Serial.println("GO DOWN");
  delay(4000);
  actuator_front.writeMicroseconds(1639);
  actuator_backL.writeMicroseconds(1361);
  actuator_backR.writeMicroseconds(1361);
  Serial.println("LEVEL");
  delay(3000);
}

void reset() {
  actuator_front.writeMicroseconds(1000);
  actuator_backL.writeMicroseconds(1000);
  actuator_backR.writeMicroseconds(1000);
}

