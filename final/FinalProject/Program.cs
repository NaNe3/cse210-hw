using System;

partial class Program
{
    private const int LinesPerPage = 15;
    private static readonly string[] ReaderControls = new string[]
    {
        "enter key:   next page",
        "backspace:   last page",
        "q:           save progress and quit"
    };

    static void Main(string[] args)
    {
        DigitalLibrary library = new DigitalLibrary();

        SeedLibrary(library);

        Console.WriteLine("Classical Digital Library - The Coolest Thing Literally Ever :D");
        Console.WriteLine("--------------------------------");

        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("  The Super Mega Library of Classical Literature  ");
            Console.WriteLine("  Created By: Eli Summers                         ");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("1. View all works");
            Console.WriteLine("2. Read a work");
            Console.WriteLine("4. View reading timeline");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            Console.Write("Selection: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ViewWorksFlow(library);
                    break;
                case "2":
                    ReadWorkFlow(library);
                    break;
                case "4":
                    ViewReadingTimelineFlow(library);
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }
        }

        Console.WriteLine("Good luck with your studies.");
    }
}