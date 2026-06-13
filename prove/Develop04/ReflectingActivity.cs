using System;
using System.Collections.Generic;
using System.Threading;

public class ReflectingActivity : Activity
{
    private readonly List<string> _prompts;
    private readonly List<string> _questions;
    private readonly Random _random;

    public ReflectingActivity()
        : base(
            "Reflecting Activity",
            "Reflect on the strengths and resilience of your day and life! Remember that you were born for greatness!")
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _questions = new List<string>
        {
            "Why was this so meaning?",
            "IS this the first time you've done this, or has it happened before?",
            "How did you get started?",
            "How did you feel afterwards?",
            "What made this time different than other times when you were not as successful?",
            "What was the highlight of this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };
    }

    public override void Run()
    {
        DisplayStartMessage();

        Console.WriteLine("Consider the following prompt:\n");
        Console.WriteLine($"--- {GetRandomPrompt()} ---\n");
        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder each of the following questions as they relate to this experience.");

        DisplayPromptQuestions();

        DisplayEndMessage();
        LogActivityCompletion();
    }

    public string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }

    public string GetRandomQuestion()
    {
        return _questions[_random.Next(_questions.Count)];
    }

    public void DisplayPromptQuestions()
    {
        int secondsBetweenQuestions = 8;
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.WriteLine($"> {GetRandomQuestion()}");
            Thread.Sleep(secondsBetweenQuestions * 1000);
        }
    }
}