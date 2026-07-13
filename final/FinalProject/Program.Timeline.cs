using System;
using System.Collections.Generic;
using System.IO;

partial class Program
{
    static void ViewReadingTimelineFlow(DigitalLibrary library)
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
        Console.Write("Press a number to view its reading timeline or press Enter to return: ");
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
        List<ReadingSession> sessions = LoadReadingSessions(selectedWork.Title);
        if (sessions.Count == 0)
        {
            Console.WriteLine($"No reading timeline found for {selectedWork.Title}.");
            PauseForContinue();
            return;
        }

        PrintReadingTimeline(selectedWork, sessions);
        PauseForContinue();
    }

    static List<ReadingSession> LoadReadingSessions(string title)
    {
        List<ReadingSession> sessions = new List<ReadingSession>();
        string historyPath = GetReadingHistoryFilePath(title);
        if (!File.Exists(historyPath))
        {
            return sessions;
        }

        string[] lines = File.ReadAllLines(historyPath);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length < 6)
            {
                continue;
            }

            DateTimeOffset startedAt;
            DateTimeOffset finishedAt;
            int savedLine;
            int furthestLineRead;
            int totalLines;
            double percentComplete;

            if (!DateTimeOffset.TryParse(parts[0], out startedAt))
            {
                continue;
            }

            if (!DateTimeOffset.TryParse(parts[1], out finishedAt))
            {
                continue;
            }

            if (!int.TryParse(parts[2], out savedLine))
            {
                continue;
            }

            if (!int.TryParse(parts[3], out furthestLineRead))
            {
                continue;
            }

            if (!int.TryParse(parts[4], out totalLines))
            {
                continue;
            }

            if (!double.TryParse(parts[5], out percentComplete))
            {
                percentComplete = totalLines <= 0 ? 0 : (double)furthestLineRead / totalLines * 100;
            }

            sessions.Add(new ReadingSession(startedAt, finishedAt, savedLine, percentComplete));
        }

        return sessions;
    }

    static void PrintReadingTimeline(LiteraryWork work, List<ReadingSession> sessions)
    {
        Console.WriteLine();
        Console.WriteLine($"Reading timeline for {work.Title}");
        Console.WriteLine("--------------------------------");

        const int timelineHeight = 20;
        List<ReadingSession>[] rows = new List<ReadingSession>[timelineHeight];
        for (int index = 0; index < timelineHeight; index++)
        {
            rows[index] = new List<ReadingSession>();
        }

        foreach (ReadingSession session in sessions)
        {
            int rowIndex = (int)Math.Round(session.PercentComplete / 100 * (timelineHeight - 1));
            rowIndex = Math.Clamp(rowIndex, 0, timelineHeight - 1);
            rows[rowIndex].Add(session);
        }

        for (int row = 0; row < timelineHeight; row++)
        {
            double percentLabel = (double)row / (timelineHeight - 1) * 100;
            Console.Write($"{percentLabel,6:0}% |");

            if (rows[row].Count == 0)
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();
            foreach (ReadingSession session in rows[row])
            {
                Console.WriteLine($"        + {session.GetDisplayText()}");
            }
        }
    }

    private sealed class ReadingSession
    {
        public DateTimeOffset StartedAt { get; }
        public DateTimeOffset FinishedAt { get; }
        public int SavedLine { get; }
        public double PercentComplete { get; }

        public ReadingSession(DateTimeOffset startedAt, DateTimeOffset finishedAt, int savedLine, double percentComplete)
        {
            StartedAt = startedAt;
            FinishedAt = finishedAt;
            SavedLine = savedLine;
            PercentComplete = percentComplete;
        }

        public string GetDisplayText()
        {
            return $"{FinishedAt:yyyy-MM-dd HH:mm} | started {StartedAt:HH:mm} | saved line {SavedLine} | read {PercentComplete:0.0}%";
        }
    }
}
