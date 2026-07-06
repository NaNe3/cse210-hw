public class LatinAuthor : Author
{
    public string EraPeriod { get; }

    public LatinAuthor(string name, int birthYear, int deathYear, string eraPeriod)
        : base(name, birthYear, deathYear)
    {
        EraPeriod = eraPeriod;
    }

    public string GetEraPeriod()
    {
        return EraPeriod;
    }
}
