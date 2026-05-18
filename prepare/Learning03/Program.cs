using System;

// THE SUPER AWESOME PROGRAM TO CREATE FRACTIONS AND STUFF!!!
class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Fraction f2 = new Fraction(5);
        Fraction f3 = new Fraction(3, 4);
        Fraction f4 = new Fraction(1, 3);

        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());

        f1.SetTop(7);
        f1.SetBottom(8);
        Console.WriteLine($"After setter: {f1.GetTop()}/{f1.GetBottom()}");

        Fraction randomFractionForTheDisplays = new Fraction();
        Random rng = new Random();

        for (int i = 1; i <= 20; i++)
        {
            randomFractionForTheDisplays.SetTop(rng.Next(1, 100));
            randomFractionForTheDisplays.SetBottom(rng.Next(1, 100));
            Console.WriteLine($"Fraction {i}: string: {randomFractionForTheDisplays.GetFractionString()} Number: {randomFractionForTheDisplays.GetDecimalValue()}");
        }
    }
}
