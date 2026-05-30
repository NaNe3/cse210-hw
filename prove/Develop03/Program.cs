using System;

// CREATIVITY:
// To make this program more creative, I decided that I wanted the give the user
// the option to select which verses they wanted to memorize. As such, I put the
// entire section of D&C Section 4 into the program. Now the user can insert a 
// span of verses that he/she feels comfortable with memorizing.

class Program
{
    private static bool _continue = true;
    private static Scripture _scripture;

    static void Main(string[] args)
    {
        _scripture = Init();

        while (_continue)
        {
            ClearScreen();
            Console.WriteLine(_scripture.DisplayVerse());

            if (_scripture.IsCompletelyHidden())
            {
                Quit();
                break;
            }

            Console.WriteLine();
            Console.Write("Press Enter to continue or type 'quit' to finish: ");
            string input = Console.ReadLine();

            if (string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase))
            {
                Quit();
            }
            else
            {
                _scripture.HideRandomWords();
            }
        }
    }

    private static Scripture Init()
    {
        Console.WriteLine("WELCOME TO THE D&C SECTION 4 SUPER MEMORIZER PROGRAM!");
        Console.WriteLine("Please select the verses you wish to memorize:\n");
        Console.Write("Start verse (1-7): ");
        int startVerse = int.Parse(Console.ReadLine());

        Console.Write("Last verse (hit enter to only memorize start verse): ");
        string endInput = Console.ReadLine();
        int endVerse = string.IsNullOrWhiteSpace(endInput) ? startVerse : int.Parse(endInput);

        return Scripture.CreateDoctrineAndCovenants4(startVerse, endVerse);
    }

    private static void ClearScreen()
    {
        Console.Clear();
    }

    private static void Quit()
    {
        _continue = false;
    }
}