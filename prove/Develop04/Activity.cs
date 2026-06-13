using System;
using System.IO;
using System.Threading;

public class Activity
{
    private readonly string _name;
    private readonly string _description;
    private int _duration;

    public static string LogFilePath => Path.Combine(Directory.GetCurrentDirectory(), "activity_log.txt");

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void DisplayStartMessage()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name}.\n");
        Console.WriteLine(_description);
        Console.WriteLine();
        Console.Write("How long, in seconds, would you like for your session? ");

        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
        {
            Console.Write("Please enter a valid positive number of seconds: ");
        }
    }

    public void DisplayEndMessage()
    {
        Console.Write("Logging activity :D ");
        ShowSpinner(2);
        Console.Clear();
        Console.WriteLine("Good job!");
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name}.");
        Console.WriteLine($"This session will be logged as: [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {_name} - {_duration} seconds");
        Console.WriteLine();
        Console.Write("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    public void ShowSpinner(int sec)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int end = sec * 5;

        for (int i = 0; i < end; i++)
        {
            Console.Write(spinner[i % spinner.Length]);
            Thread.Sleep(200);
            Console.Write("\b \b");
        }
    }

    public void ShowCountdown(int sec)
    {
        for (int i = sec; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b\b");
        }

        Console.Write("   \n");
    }

    public int GetDuration()
    {
        return _duration;
    }

    public virtual void Run()
    {
    }

    public void LogActivityCompletion()
    {
        string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {_name} - {_duration} seconds";
        File.AppendAllLines(LogFilePath, new[] { logLine });
    }
}