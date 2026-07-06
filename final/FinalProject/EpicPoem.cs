public class EpicPoem : LiteraryWork
{
    public int NumBooks { get; }

    public EpicPoem(string title, int totalUnits, string language, Author author, int numBooks)
        : base(title, totalUnits, language, author)
    {
        NumBooks = numBooks;
    }

    public override string GetSummary()
    {
        return $"[EpicPoem] {Title} by {GetAuthor().Name} ({Language}) - {NumBooks} books";
    }
}
