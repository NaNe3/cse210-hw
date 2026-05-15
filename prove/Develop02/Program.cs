using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // To exceed the requirements of this course, I added three new options
        // Search by date, Search by content, and Display index range
        Journal journal = new Journal();
        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the worst crime which you committed today?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What small win am I proud of today?",
            "How many times did you hear the conjunction 'but' today?"
        };

        bool keepRunning = true;
        while (keepRunning)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Search by date");
            Console.WriteLine("6. Search by text");
            Console.WriteLine("7. Display index range");
            Console.WriteLine("8. Quit");
            Console.Write("What would you like to do? ");

            string option = Console.ReadLine() ?? string.Empty;
            keepRunning = InterpretOptionSelected(option.Trim(), journal, prompts);
        }
    }

    public static List<string> ReadAllEntries(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return new List<string>();
        }

        return new List<string>(File.ReadAllLines(fileName));
    }

    public static void WriteEntriesToFile(string fileName, Journal journal)
    {
        string separator = GetSeparator();
        List<string> lines = new List<string>();

        foreach (Entry entry in journal.Entries)
        {
            string safeContent = entry.EntryContent.Replace(Environment.NewLine, "\\n");
            string line = $"{entry.DateString}{separator}{entry.PromptId}{separator}{safeContent}";
            lines.Add(line);
        }

        File.WriteAllLines(fileName, lines);
    }

    public static bool InterpretOptionSelected(string option, Journal journal, List<string> prompts)
    {
        switch (option)
        {
            case "1":
                string prompt = GenerateRandomPrompt(prompts, out int promptId);
                Console.WriteLine();
                Console.WriteLine($"Prompt: {prompt}");
                Console.Write("> ");
                string response = Console.ReadLine() ?? string.Empty;
                journal.CreateNewEntry(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), promptId, response);
                Console.WriteLine("Entry saved in memory.");
                return true;

            case "2":
                Console.WriteLine();
                if (journal.Entries.Count == 0)
                {
                    Console.WriteLine("No entries found.");
                    return true;
                }

                for (int i = 0; i < journal.Entries.Count; i++)
                {
                    journal.Entries[i].DisplayEntry(i, prompts);
                }
                return true;

            case "3":
                Console.Write("Enter filename to load: ");
                string loadFile = Console.ReadLine() ?? string.Empty;
                List<string> rawEntries = ReadAllEntries(loadFile);
                journal.FormatJournalEntries(rawEntries, GetSeparator());
                Console.WriteLine($"Loaded {journal.Entries.Count} entries.");
                return true;

            case "4":
                Console.Write("Enter filename to save: ");
                string saveFile = Console.ReadLine() ?? string.Empty;
                WriteEntriesToFile(saveFile, journal);
                Console.WriteLine("Journal saved.");
                return true;

            case "5":
                Console.Write("Enter date text to search (example: 2026-05-10): ");
                string dateText = Console.ReadLine() ?? string.Empty;
                List<Entry> dateMatches = journal.GetEntryByDateString(dateText);
                DisplayEntries(dateMatches, prompts, "date search");
                return true;

            case "6":
                Console.Write("Search: ");
                string pattern = Console.ReadLine() ?? string.Empty;
                try
                {
                    List<Entry> regexMatches = journal.GetEntriesByContentRegex(pattern);
                    DisplayEntries(regexMatches, prompts, "content regex search");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid regex pattern.");
                }
                return true;

            case "7":
                Console.Write("Start index (0-based): ");
                string startInput = Console.ReadLine() ?? string.Empty;
                Console.Write("End index (0-based): ");
                string endInput = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(startInput, out int startIndex) && int.TryParse(endInput, out int endIndex))
                {
                    List<Entry> spanEntries = journal.GetEntriesByIndexSpan(startIndex, endIndex);
                    DisplayEntries(spanEntries, prompts, "index range");
                }
                else
                {
                    Console.WriteLine("Please provide valid integer indexes.");
                }
                return true;

            case "8":
                Console.WriteLine("Goodbye!");
                return false;

            default:
                Console.WriteLine("Invalid option. Please choose a menu number.");
                return true;
        }
    }

    public static string GenerateRandomPrompt(List<string> prompts, out int promptId)
    {
        Random random = new Random();
        promptId = random.Next(prompts.Count);
        return prompts[promptId];
    }

    private static string GetSeparator()
    {
        return "~|~";
    }

    private static void DisplayEntries(List<Entry> entries, List<string> prompts, string label)
    {
        Console.WriteLine();
        if (entries.Count == 0)
        {
            Console.WriteLine($"No entries found for {label}.");
            return;
        }

        Console.WriteLine($"Found {entries.Count} entries for {label}:");
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].DisplayEntry(i, prompts);
        }
    }

    // DisplayEntry method moved to Entry class as an instance method.
}