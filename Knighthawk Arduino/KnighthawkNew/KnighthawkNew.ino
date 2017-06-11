#include <Servo.h>

int actuator_pin = 9;
Servo actuator;

void setup() {
  Serial.begin(9600);
  Serial.println("Attaching actuators...");
  actuator.attach(actuator_pin);
  Serial.println("Attached.");
}

void loop() {
  while (Serial.available() != 0) {
    int val = Serial.parseInt();
    Serial.println(val);
    actuator_backL.writeMicroseconds(val);
    actuator_backR.writeMicroseconds(val);
  }
}

