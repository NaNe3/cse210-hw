using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int theNumberToGuess = randomGenerator.Next(1, 101);

        int userGuess = -1;

        while (userGuess != theNumberToGuess)
        {
            Console.Write("What is your guess? ");
            userGuess = int.Parse(Console.ReadLine());

            if (theNumberToGuess > userGuess)
            {
                Console.WriteLine("Higher");
            }
            else if (theNumberToGuess < userGuess)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("Congrats!! You guessed it!");
            }

        }                    
    }
}