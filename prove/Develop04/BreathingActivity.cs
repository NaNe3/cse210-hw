using System;

public class BreathingActivity : Activity
{
    private int _breathSec;

    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
        _breathSec = 4;
    }

    public override void Run()
    {
        DisplayStartMessage();

        int elapsed = 0;
        int duration = GetDuration();

        while (elapsed < duration)
        {
            BreatheIn();
            elapsed += _breathSec;

            if (elapsed >= duration)
            {
                break;
            }

            BreatheOut();
            elapsed += _breathSec;
        }

        DisplayEndMessage();
        LogActivityCompletion();
    }

    public void BreatheIn()
    {
        Console.Write("\nBreathe in...\n");
        ShowCountdown(_breathSec);
    }

    public void BreatheOut()
    {
        Console.Write("\nBreathe out...\n");
        ShowCountdown(_breathSec);
    }
}