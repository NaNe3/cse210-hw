using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private readonly List<string> _prompts;
    private readonly Random _random;
    private int _itemCount;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _random = new Random();

        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _itemCount = 0;
    }

    public override void Run()
    {
        DisplayStartMessage();

        Console.WriteLine("List as many responses as you can to the following prompt:\n");
        Console.WriteLine($"--- {GetRandomPrompt()} ---\n");

        _itemCount = CountItems();

        Console.WriteLine($"\nYou listed {_itemCount} items!");

        DisplayEndMessage();
        LogActivityCompletion();
    }

    public string GetRandomPrompt()
    {
        return _prompts[_random.Next(_prompts.Count)];
    }

    public int CountItems()
    {
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        int count = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string response = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(response))
            {
                count++;
            }
        }

        return count;
    }
}