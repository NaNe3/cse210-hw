public abstract class LiteraryWork
{
    public string Title { get; }
    public int TotalUnits { get; }
    public string Language { get; }
    private readonly Author _author;

    protected LiteraryWork(string title, int totalUnits, string language, Author author)
    {
        Title = title;
        TotalUnits = totalUnits;
        Language = language;
        _author = author;
        _author.AddWork(this);
    }

    public Author GetAuthor()
    {
        return _author;
    }

    public abstract string GetSummary();
}
