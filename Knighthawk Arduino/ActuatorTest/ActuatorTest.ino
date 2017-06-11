// Knighthawk Actuator

#include <Servo.h>

Servo actuator;
int speed = 1600;

int incomingByte = 0;

void setup() {
  Serial.begin(9600);
  Serial.println("Attaching...");
  actuator.attach(9);
  Serial.println("Attached");
}

void loop() {
  input();
}

void input() {
  while (Serial.available() != 0) {
    int val = Serial.parseInt();
    actuator.writeMicroseconds(val);
    Serial.println(val);
  }
}

void straight() {
  actuator.writeMicroseconds(speed);
  Serial.println(speed);
  delay(25);
}

void test() {
  actuator.writeMicroseconds(1667);
  delay(6500);
  actuator.writeMicroseconds(1500);
}

void pulse() {
  Serial.println("Retracting...");
  for (int pos = 2000; pos >= 1000; pos -= 1) {
    actuator.writeMicroseconds(pos);
    Serial.println(pos);
    delay(25);
  }
  Serial.println("Retracted");
  Serial.println("Extending...");
  for (int pos = 1000; pos <= 2000; pos += 1) {
    actuator.writeMicroseconds(pos);
    Serial.println(pos);
    delay(25);
  }
  Serial.println("Extended");
  delay(25);
}

