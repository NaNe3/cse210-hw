using System;
using System.Collections.Generic;
using System.IO;

partial class Program
{
    static void ViewWorksFlow(DigitalLibrary library)
    {
        List<LiteraryWork> works = library.GetWorks();
        if (works.Count == 0)
        {
            Console.WriteLine("No works found.");
            PauseForContinue();
            return;
        }

        for (int index = 0; index < works.Count; index++)
        {
            Console.WriteLine($"{index + 1}. {works[index].GetSummary()}");
        }

        Console.WriteLine();
        Console.Write("Press a number to open a work or press Enter to return: ");
        string selection = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(selection))
        {
            return;
        }

        int selectedIndex;
        if (!int.TryParse(selection, out selectedIndex) || selectedIndex < 1 || selectedIndex > works.Count)
        {
            Console.WriteLine("Please choose a valid work number.");
            PauseForContinue();
            return;
        }

        LiteraryWork selectedWork = works[selectedIndex - 1];
        string filePath = GetLiteratureFilePath(selectedWork.Title);
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Could not find the text file for {selectedWork.Title}.");
            Console.WriteLine($"Expected file: {filePath}");
            PauseForContinue();
            return;
        }

        ReadTextFile(selectedWork.Title, filePath);
    }

    static void ReadWorkFlow(DigitalLibrary library)
    {
        Console.Write("Work title: ");
        string title = Console.ReadLine();

        LiteraryWork work = FindWorkByTitle(library, title);
        if (work == null)
        {
            Console.WriteLine("Work not found.");
            return;
        }

        string filePath = GetLiteratureFilePath(work.Title);
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Could not find the text file for {work.Title}.");
            Console.WriteLine($"Expected file: {filePath}");
            return;
        }

        ReadTextFile(work.Title, filePath);
    }

    static LiteraryWork FindWorkByTitle(DigitalLibrary library, string title)
    {
        foreach (LiteraryWork work in library.GetWorks())
        {
            if (work.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                return work;
            }
        }

        return null;
    }

    static void PauseForContinue()
    {
        Console.WriteLine();
        Console.Write("Press Enter to return to the menu...");
        Console.ReadLine();
    }
}
