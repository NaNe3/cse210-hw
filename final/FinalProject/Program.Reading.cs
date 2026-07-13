using System;
using System.IO;

partial class Program
{
    static void ReadTextFile(string title, string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int currentLine = LoadReadingProgress(title, lines.Length);
        int lastPageStart = GetLastPageStart(lines.Length);
        DateTimeOffset sessionStartedAt = DateTimeOffset.Now;

        if (currentLine > lastPageStart)
        {
            currentLine = lastPageStart;
        }

        int furthestLineRead = currentLine;

        while (true)
        {
            Console.Clear();

            int endLine = Math.Min(currentLine + LinesPerPage, lines.Length);
            furthestLineRead = endLine;
            for (int index = currentLine; index < endLine; index++)
            {
                Console.WriteLine(lines[index]);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("============================================");
            foreach (string control in ReaderControls)
            {
                Console.WriteLine(control);
            }

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Q)
            {
                SaveReadingProgress(title, currentLine, furthestLineRead, lines.Length, sessionStartedAt, DateTimeOffset.Now);
                return;
            }

            if (key == ConsoleKey.Enter)
            {
                if (currentLine < lastPageStart)
                {
                    currentLine += LinesPerPage;
                }

                continue;
            }

            if (key == ConsoleKey.Backspace)
            {
                currentLine = Math.Max(0, currentLine - LinesPerPage);
            }
        }
    }

    static int LoadReadingProgress(string title, int totalLines)
    {
        string progressPath = GetReadingProgressFilePath(title);
        if (!File.Exists(progressPath))
        {
            return 0;
        }

        string savedValue = File.ReadAllText(progressPath).Trim();
        int savedLine;
        if (!int.TryParse(savedValue, out savedLine))
        {
            return 0;
        }

        return Math.Clamp(savedLine, 0, GetLastPageStart(totalLines));
    }

    static void SaveReadingProgress(string title, int currentLine, int furthestLineRead, int totalLines, DateTimeOffset startedAt, DateTimeOffset finishedAt)
    {
        string progressPath = GetReadingProgressFilePath(title);
        string progressDirectory = Path.GetDirectoryName(progressPath);
        if (!string.IsNullOrEmpty(progressDirectory))
        {
            Directory.CreateDirectory(progressDirectory);
        }

        File.WriteAllText(progressPath, currentLine.ToString());

        string historyPath = GetReadingHistoryFilePath(title);
        string historyDirectory = Path.GetDirectoryName(historyPath);
        if (!string.IsNullOrEmpty(historyDirectory))
        {
            Directory.CreateDirectory(historyDirectory);
        }

        double percentComplete = totalLines <= 0 ? 0 : (double)furthestLineRead / totalLines * 100;
        string historyLine = string.Join("|", new string[]
        {
            startedAt.ToString("o"),
            finishedAt.ToString("o"),
            currentLine.ToString(),
            furthestLineRead.ToString(),
            totalLines.ToString(),
            percentComplete.ToString("0.0")
        });

        File.AppendAllText(historyPath, historyLine + Environment.NewLine);
    }

    static int GetLastPageStart(int totalLines)
    {
        if (totalLines <= 0)
        {
            return 0;
        }

        return ((totalLines - 1) / LinesPerPage) * LinesPerPage;
    }

    static string GetLiteratureFilePath(string title)
    {
        return Path.Combine(GetProjectRoot(), "literature", $"{ToFileName(title)}.txt");
    }

    static string GetReadingProgressFilePath(string title)
    {
        return Path.Combine(GetProjectRoot(), "reading-progress", $"{ToFileName(title)}.txt");
    }

    static string GetReadingHistoryFilePath(string title)
    {
        return Path.Combine(GetProjectRoot(), "reading-history", $"{ToFileName(title)}.txt");
    }

    static string GetProjectRoot()
    {
        DirectoryInfo current = new DirectoryInfo(AppContext.BaseDirectory);

        while (current != null)
        {
            string projectFile = Path.Combine(current.FullName, "FinalProject.csproj");
            string literatureFolder = Path.Combine(current.FullName, "literature");

            if (File.Exists(projectFile) || Directory.Exists(literatureFolder))
            {
                return current.FullName;
            }

            current = current.Parent;
        }

        return Directory.GetCurrentDirectory();
    }

    static string ToFileName(string value)
    {
        char[] invalidCharacters = Path.GetInvalidFileNameChars();
        char[] characters = value.Trim().Replace(' ', '_').ToCharArray();

        for (int index = 0; index < characters.Length; index++)
        {
            if (Array.IndexOf(invalidCharacters, characters[index]) >= 0)
            {
                characters[index] = '_';
            }
        }

        return new string(characters);
    }
}
