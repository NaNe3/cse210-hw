public class Entry
{
    private readonly string _dateString;
    private readonly int _promptId;
    private readonly string _entryContent;

    public string DateString => _dateString;
    public int PromptId => _promptId;
    public string EntryContent => _entryContent;

    public Entry(string dateString, int promptId, string entryContent)
    {
        _dateString = dateString;
        _promptId = promptId;
        _entryContent = entryContent;
    }
    public void DisplayEntry(int index, List<string> prompts)
    {
        string promptText = _promptId >= 0 && _promptId < prompts.Count
            ? prompts[_promptId]
            : "Unknown prompt";

        Console.WriteLine($"[{index}] Date: {_dateString}");
        Console.WriteLine($"Prompt: {promptText}");
        Console.WriteLine($"Entry: {_entryContent}");
        Console.WriteLine();
    }
}
