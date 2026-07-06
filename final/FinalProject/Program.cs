using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        DigitalLibrary library = new DigitalLibrary();
        Student student = new Student("Eli");

        SeedLibrary(library, student);

        Console.WriteLine("Classical Digital Library - The Coolest Thing Literally Ever :D");
        Console.WriteLine("--------------------------------");

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. View all works");
            Console.WriteLine("2. Search works by author");
            Console.WriteLine("3. Search works by genre");
            Console.WriteLine("4. Add a new work");
            Console.WriteLine("5. Track progress on a work");
            Console.WriteLine("6. View progress records");
            Console.WriteLine("7. View study roadmap");
            Console.WriteLine("8. Exit");
            Console.Write("Selection: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    PrintWorks(library.GetWorks());
                    break;
                case "2":
                    SearchByAuthor(library);
                    break;
                case "3":
                    SearchByGenre(library);
                    break;
                case "4":
                    AddNewWorkFlow(library, student);
                    break;
                case "5":
                    TrackProgressFlow(student);
                    break;
                case "6":
                    PrintProgress(student);
                    break;
                case "7":
                    PrintRoadmap(student);
                    break;
                case "8":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }
        }

        Console.WriteLine("Good luck with your studies.");
    }

    static void SeedLibrary(DigitalLibrary library, Student student)
    {
        GreekAuthor homer = new GreekAuthor("Homer", -750, -701, "Epic Greek");
        GreekAuthor sophocles = new GreekAuthor("Sophocles", -497, -406, "Attic Greek");
        LatinAuthor virgil = new LatinAuthor("Virgil", -70, -19, "Augustan");
        LatinAuthor cicero = new LatinAuthor("Cicero", -106, -43, "Late Republic");

        LiteraryWork iliad = new EpicPoem("Iliad", 24, "Greek", homer, 24);
        LiteraryWork odyssey = new EpicPoem("Odyssey", 24, "Greek", homer, 24);
        LiteraryWork oedipusRex = new Tragedy("Oedipus Rex", 1, "Greek", sophocles, 5);
        LiteraryWork antigone = new Tragedy("Antigone", 1, "Greek", sophocles, 5);
        LiteraryWork aeneid = new EpicPoem("Aeneid", 12, "Latin", virgil, 12);
        LiteraryWork georgics = new EpicPoem("Georgics", 4, "Latin", virgil, 4);
        LiteraryWork deOratore = new OratoricalWork("De Oratore", 3, "Latin", cicero, "Roman Senate and Statesmen");
        LiteraryWork philippics = new OratoricalWork("Philippics", 14, "Latin", cicero, "Roman Senate");

        library.AddAuthor(homer);
        library.AddAuthor(sophocles);
        library.AddAuthor(virgil);
        library.AddAuthor(cicero);

        library.AddWork(iliad);
        library.AddWork(odyssey);
        library.AddWork(oedipusRex);
        library.AddWork(antigone);
        library.AddWork(aeneid);
        library.AddWork(georgics);
        library.AddWork(deOratore);
        library.AddWork(philippics);

        student.AddNewWork(iliad);
        student.AddNewWork(aeneid);
        student.AddNewWork(deOratore);
        student.TrackProgress("Iliad", 6);
        student.TrackProgress("Aeneid", 3);
        student.TrackProgress("De Oratore", 1);

        StudyRoadmap roadmap = new StudyRoadmap();
        roadmap.AddMilestone(new Milestone("Finish Iliad books 1-12", new DateTime(2026, 8, 15)));
        roadmap.AddMilestone(new Milestone("Complete Oedipus Rex analysis notes", new DateTime(2026, 9, 1)));
        roadmap.AddMilestone(new Milestone("Finish Aeneid", new DateTime(2026, 11, 30)));
        student.AddRoadmap(roadmap);
    }

    static void PrintWorks(List<LiteraryWork> works)
    {
        if (works.Count == 0)
        {
            Console.WriteLine("No works found.");
            return;
        }

        foreach (LiteraryWork work in works)
        {
            Console.WriteLine(work.GetSummary());
        }
    }

    static void SearchByAuthor(DigitalLibrary library)
    {
        Console.Write("Enter author name: ");
        string name = Console.ReadLine();
        List<LiteraryWork> results = library.SearchByAuthor(name);
        PrintWorks(results);
    }

    static void SearchByGenre(DigitalLibrary library)
    {
        Console.Write("Enter genre (EpicPoem, Tragedy, OratoricalWork): ");
        string genre = Console.ReadLine();
        List<LiteraryWork> results = library.SearchByGenre(genre);
        PrintWorks(results);
    }

    static void AddNewWorkFlow(DigitalLibrary library, Student student)
    {
        Console.Write("Title: ");
        string title = Console.ReadLine();

        Console.Write("Total units (books/sections): ");
        int totalUnits = ParseIntOrDefault(Console.ReadLine(), 1);

        Console.Write("Language: ");
        string language = Console.ReadLine();

        Console.Write("Author name: ");
        string authorName = Console.ReadLine();

        Author author = FindOrCreateAuthor(library, authorName);

        Console.Write("Work type (1=EpicPoem, 2=Tragedy, 3=OratoricalWork): ");
        string typeChoice = Console.ReadLine();

        LiteraryWork work;
        if (typeChoice == "1")
        {
            Console.Write("Number of books: ");
            int numBooks = ParseIntOrDefault(Console.ReadLine(), totalUnits);
            work = new EpicPoem(title, totalUnits, language, author, numBooks);
        }
        else if (typeChoice == "2")
        {
            Console.Write("Number of acts: ");
            int numActs = ParseIntOrDefault(Console.ReadLine(), 5);
            work = new Tragedy(title, totalUnits, language, author, numActs);
        }
        else
        {
            Console.Write("Audience: ");
            string audience = Console.ReadLine();
            work = new OratoricalWork(title, totalUnits, language, author, audience);
        }

        library.AddWork(work);
        student.AddNewWork(work);

        Console.WriteLine("Work added and tracking started.");
        Console.WriteLine("Create a matching empty .txt file in the project for this new work when you are ready.");
    }

    static Author FindOrCreateAuthor(DigitalLibrary library, string authorName)
    {
        foreach (Author author in library.GetAuthors())
        {
            if (author.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase))
            {
                return author;
            }
        }

        Console.Write("New author type (1=Greek, 2=Latin): ");
        string newType = Console.ReadLine();
        Console.Write("Birth year: ");
        int birthYear = ParseIntOrDefault(Console.ReadLine(), 0);
        Console.Write("Death year: ");
        int deathYear = ParseIntOrDefault(Console.ReadLine(), 0);

        Author created;
        if (newType == "1")
        {
            Console.Write("Dialect: ");
            string dialect = Console.ReadLine();
            created = new GreekAuthor(authorName, birthYear, deathYear, dialect);
        }
        else
        {
            Console.Write("Era period: ");
            string eraPeriod = Console.ReadLine();
            created = new LatinAuthor(authorName, birthYear, deathYear, eraPeriod);
        }

        library.AddAuthor(created);
        return created;
    }

    static void TrackProgressFlow(Student student)
    {
        Console.Write("Work title: ");
        string title = Console.ReadLine();
        Console.Write("Units completed since last update: ");
        int units = ParseIntOrDefault(Console.ReadLine(), 0);

        bool updated = student.TrackProgress(title, units);
        if (!updated)
        {
            Console.WriteLine("Work not found in your records.");
            return;
        }

        Console.WriteLine("Progress updated.");
    }

    static void PrintProgress(Student student)
    {
        List<ProgressRecord> records = student.GetRecords();
        if (records.Count == 0)
        {
            Console.WriteLine("No progress records yet.");
            return;
        }

        foreach (ProgressRecord record in records)
        {
            Console.WriteLine(record.GetDisplayLine());
        }
    }

    static void PrintRoadmap(Student student)
    {
        List<StudyRoadmap> roadmaps = student.GetRoadmaps();
        if (roadmaps.Count == 0)
        {
            Console.WriteLine("No roadmap found.");
            return;
        }

        foreach (StudyRoadmap roadmap in roadmaps)
        {
            Console.WriteLine($"Roadmap created {roadmap.CreatedDate:yyyy-MM-dd}");
            foreach (Milestone milestone in roadmap.GetMilestones())
            {
                string status = milestone.IsCompleted ? "done" : "pending";
                Console.WriteLine($"- {milestone.Description} (target {milestone.TargetDate:yyyy-MM-dd}, {status})");
            }

            Milestone next = roadmap.NextMilestone();
            if (next != null)
            {
                Console.WriteLine($"Next milestone: {next.Description} ({next.TargetDate:yyyy-MM-dd})");
            }
        }
    }

    static int ParseIntOrDefault(string value, int fallback)
    {
        int parsed;
        if (int.TryParse(value, out parsed))
        {
            return parsed;
        }

        return fallback;
    }
}