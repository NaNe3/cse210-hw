using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment firstAssignment = new Assignment("Samuel Bennett", "Multiplication");
        Console.WriteLine(firstAssignment.GetSummary());

        Console.WriteLine();

        MathAssignment secondAssignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(secondAssignment.GetSummary());
        Console.WriteLine(secondAssignment.GetHomeworkList());

        Console.WriteLine();

        WritingAssignment thirdAssignment = new WritingAssignment(
            "Mary Waters",
            "European History",
            "The Causes of World War II"
        );
        Console.WriteLine(thirdAssignment.GetSummary());
        Console.WriteLine(thirdAssignment.GetWritingInformation());
    }
}