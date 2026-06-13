using System;
using System.IO;

class Program
{
    private static bool _continue = true;
    private static Activity _activity = null!;

    static void Main(string[] args)
    {
        while (_continue)
        {
            DisplayMenu();
            RunActivity();
        }
    }

    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Start breathing activity");
        Console.WriteLine("  2. Start reflecting activity");
        Console.WriteLine("  3. Start listing activity");
        Console.WriteLine("  4. Display logged activities");
        Console.WriteLine("  5. Quit");
        Console.Write("Select a choice from the menu: ");
    }

    static void RunActivity()
    {
        string choice = Console.ReadLine() ?? string.Empty;

        switch (choice)
        {
            case "1":
                _activity = new BreathingActivity();
                _activity.Run();
                break;
            case "2":
                _activity = new ReflectingActivity();
                _activity.Run();
                break;
            case "3":
                _activity = new ListingActivity();
                _activity.Run();
                break;
            case "4":
                DisplayLoggedActivities();
                break;
            case "5":
                _continue = false;
                break;
            default:
                Console.WriteLine("Invalid choice. Press Enter to continue.");
                Console.ReadLine();
                break;
        }
    }

    // CREATIVITY LOL
    // completed activities are saved to activity_log.txt and this menu option lets the user review their activity history across sessions.
    static void DisplayLoggedActivities()
    {
        Console.Clear();
        Console.WriteLine("Activity Log:\n");

        if (File.Exists(Activity.LogFilePath))
        {
            string[] lines = File.ReadAllLines(Activity.LogFilePath);

            if (lines.Length == 0)
            {
                Console.WriteLine("No logged activities yet.");
            }
            else
            {
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
        }
        else
        {
            Console.WriteLine("No logged activities yet.");
        }

        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }
}