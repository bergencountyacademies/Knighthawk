// Knighthawk Actuator

#include <Servo.h>

Servo actuator;

char pos = 0;

void setup() {
  Serial.begin(9600);
  Serial.println("Attaching...");
  actuator.attach(9);
  Serial.println("Attached");
}

void loop() {
  pos = Serial.read();
  if (pos == 48) {
    actuator.write(0);
    Serial.println("Received LOW");
  } else if (pos == 49) {
    actuator.write(180);
    Serial.println("Received HIGH");
  }
  delay(25);
}
