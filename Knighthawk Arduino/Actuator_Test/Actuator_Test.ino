// Knighthawk Actuator Test

#include <Servo.h>

Servo myservo;

int pos = 0;

void setup() {
  Serial.begin(9600);
  Serial.println("Attaching...");
  myservo.attach(9);
  Serial.println("Attached");
}

void loop() {
  Serial.println("Extending...");
  for (pos = 0; pos <= 180; pos += 1) {
    myservo.write(pos);
    delay(25);
  }
  Serial.println("Extended");
  Serial.println("Retracting...");
  for (pos = 180; pos >= 0; pos -= 1) {
    myservo.write(pos);
    delay(25);
  }
  delay(25);
  Serial.println("Retracted");
}
