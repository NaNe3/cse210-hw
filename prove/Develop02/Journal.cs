using System.Text.RegularExpressions;

public class Journal
{
    private readonly List<Entry> _entries = new List<Entry>();

    public IReadOnlyList<Entry> Entries => _entries.AsReadOnly();

    public void FormatJournalEntries(IEnumerable<string> rawEntryLines, string separator)
    {
        _entries.Clear();

        foreach (string rawLine in rawEntryLines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
            {
                continue;
            }

            string[] parts = rawLine.Split(separator, StringSplitOptions.None);
            if (parts.Length < 3)
            {
                continue;
            }

            string dateString = parts[0];
            if (!int.TryParse(parts[1], out int promptId))
            {
                continue;
            }

            string content = parts[2].Replace("\\n", Environment.NewLine);
            _entries.Add(new Entry(dateString, promptId, content));
        }
    }

    public void CreateNewEntry(string dateString, int promptId, string entryContent)
    {
        _entries.Add(new Entry(dateString, promptId, entryContent));
    }

    public Entry GetEntryByIndex(int index)
    {
        if (index < 0 || index >= _entries.Count)
        {
            return null;
        }

        return _entries[index];
    }

    public List<Entry> GetEntryByDateString(string dateString)
    {
        return _entries.FindAll(entry => entry.DateString.Contains(dateString, StringComparison.OrdinalIgnoreCase));
    }

    public List<Entry> GetEntriesByIndexSpan(int startIndex, int endIndex)
    {
        List<Entry> results = new List<Entry>();

        if (_entries.Count == 0)
        {
            return results;
        }

        int safeStart = Math.Max(0, startIndex);
        int safeEnd = Math.Min(_entries.Count - 1, endIndex);

        if (safeStart > safeEnd)
        {
            return results;
        }

        for (int i = safeStart; i <= safeEnd; i++)
        {
            results.Add(_entries[i]);
        }

        return results;
    }

    public List<Entry> GetEntriesByContentRegex(string pattern)
    {
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
        return _entries.FindAll(entry => regex.IsMatch(entry.EntryContent));
    }
}
