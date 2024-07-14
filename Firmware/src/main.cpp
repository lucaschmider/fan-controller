#include <Arduino.h>
#include <fan_controller/fan_controller.h>
#include <constants.h>

FanController fanController = FanController();

void onRevolution()
{
  fanController.onRevolution();
}

void setup()
{
  pinMode(TACHOMETER_PIN, INPUT);
  attachInterrupt(digitalPinToInterrupt(TACHOMETER_PIN), onRevolution, RISING);

  Serial.begin(9600);
}

void loop()
{
  for (int dutyCycle = 0; dutyCycle <= 100; dutyCycle++)
  {
    fanController.setDutyCycle(dutyCycle);
    for (int i = 0; i < 100; i++)
    {
      Serial.println("dutyCycle:" + String(dutyCycle) + ",rpm:" + String(fanController.getSpeed()));
      delay(250);
    }
  }
}
