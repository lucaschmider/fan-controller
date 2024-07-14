class FanController
{

private:
    volatile long lastTachometerTrigger = 0;
    volatile int speed = 0;
    volatile int dutyCycle = 0;

public:
    FanController();
    ~FanController();
    void setDutyCycle(int dutyCycle);
    void tick();
    void onRevolution();
    int getSpeed();
};