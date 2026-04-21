using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string firstNameOfThePersonLol = Console.ReadLine();
        
        Console.Write("What is your last name? ");
        string lastNameOfThePersonLol = Console.ReadLine();
        
        Console.WriteLine($"Your name is {lastNameOfThePersonLol}, {firstNameOfThePersonLol} {lastNameOfThePersonLol}.");
    }
}