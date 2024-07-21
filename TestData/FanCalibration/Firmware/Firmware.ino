#define TACHOMETER_PIN 2
#define CONTROL_PIN 11
#define CONSTANT_DELAY 1000
#define ITERATION_COUNT 10
#define MEASUREMENTS_PER_DUTY_CYCLE 10

volatile int speed = 0;
volatile long lastTachometerTrigger = 0;

void setup() {
  // Initialize PWM at Timer1
  TCCR1A = 0xA2;
  TCCR1B = 0x19;
  TCNT1H = 0x00;
  TCNT1L = 0x00;

  // Initialize PWM Pin
  pinMode(CONTROL_PIN, OUTPUT);

  // Initialize Tachometer
  lastTachometerTrigger = millis();
  pinMode(TACHOMETER_PIN, INPUT);
  attachInterrupt(digitalPinToInterrupt(TACHOMETER_PIN), onRevolution, RISING);

  // Initialize serial
  Serial.begin(9600);
}

void loop() {
  Serial.println("clear");

  for(int iterationIndex = 0; interationIndex <= ITERATION_COUNT; iterationIndex++) {
    for(int dutyCycle = 0; dutyCycle <= 100; dutyCycle++) {
      setDutyCycle(dutyCycle);
      for(int iteration = 0; iteration < MEASUREMENTS_PER_DUTY_CYCLE; iteration++) {
        delay(1000);
        Serial.println("dutyCycle:" + String(dutyCycle) + ",rpm:" + String(speed) + ",iterationIndex:" + String(iterationIndex) + ",measurementIndex:" + String(iteration));
      }
    }
  }  
  
  while(true){
    Serial.println("done");
    delay(1000);
  }
}

void setDutyCycle(int dutyCycle) {
  // icr and frecuency values are set to generate a 25 kHZ signal
  int icr = 639;

  ICR1H = icr >> 8;
  ICR1L = icr & 0x00ff;

  // Sets the duty cycle with the value entered in line 12
  OCR1A = icr * (dutyCycle / 100.0);
}

void onRevolution() {
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