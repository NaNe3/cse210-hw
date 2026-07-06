public class Tragedy : LiteraryWork
{
    public int NumActs { get; }

    public Tragedy(string title, int totalUnits, string language, Author author, int numActs)
        : base(title, totalUnits, language, author)
    {
        NumActs = numActs;
    }

    public override string GetSummary()
    {
        return $"[Tragedy] {Title} by {GetAuthor().Name} ({Language}) - {NumActs} acts";
    }
}
