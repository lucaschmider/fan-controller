#include <Arduino.h>
#include <constants.h>
#include <fan_controller/fan_controller.h>

FanController::FanController()
{
    // Initialize PWM at Timer1
    TCCR1A = 0xA2;
    TCCR1B = 0x19;
    TCNT1H = 0x00;
    TCNT1L = 0x00;

    // Initialize PWM Pin
    pinMode(CONTROL_PIN, OUTPUT);

    // Initialize Tachometer
    lastTachometerTrigger = millis();
}

FanController::~FanController()
{
}

void FanController::tick()
{
}

void FanController::setDutyCycle(int dutyCycle)
{
    // icr and frecuency values are set to generate a 25 kHZ signal
    int icr = 639;

    ICR1H = icr >> 8;
    ICR1L = icr & 0x00ff;

    // Sets the duty cycle with the value entered in line 12
    OCR1A = icr * (dutyCycle / 100.0);
}

int FanController::getSpeed()
{
    return speed;
}

void FanController::onRevolution()
{
    long now = millis();
    // Multiply by two because the tachometer triggers twice per revolution
    int cycleTime = (now - lastTachometerTrigger) * 2;
    lastTachometerTrigger = now;

    if (cycleTime <= 0)
    {
        return;
    }

    int frequency_Hz = 1000 / cycleTime;
    speed = frequency_Hz * 60;
}
