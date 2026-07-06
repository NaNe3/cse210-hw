public class OratoricalWork : LiteraryWork
{
    public string Audience { get; }

    public OratoricalWork(string title, int totalUnits, string language, Author author, string audience)
        : base(title, totalUnits, language, author)
    {
        Audience = audience;
    }

    public override string GetSummary()
    {
        return $"[OratoricalWork] {Title} by {GetAuthor().Name} ({Language}) - Audience: {Audience}";
    }
}
