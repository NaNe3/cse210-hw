public class GreekAuthor : Author
{
    public string Dialect { get; }

    public GreekAuthor(string name, int birthYear, int deathYear, string dialect)
        : base(name, birthYear, deathYear)
    {
        Dialect = dialect;
    }

    public string GetDialect()
    {
        return Dialect;
    }
}
