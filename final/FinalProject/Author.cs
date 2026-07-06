using System.Collections.Generic;

public abstract class Author
{
    private readonly List<LiteraryWork> _works = new List<LiteraryWork>();

    public string Name { get; }
    public int BirthYear { get; }
    public int DeathYear { get; }

    protected Author(string name, int birthYear, int deathYear)
    {
        Name = name;
        BirthYear = birthYear;
        DeathYear = deathYear;
    }

    public void AddWork(LiteraryWork work)
    {
        if (!_works.Contains(work))
        {
            _works.Add(work);
        }
    }

    public List<LiteraryWork> GetWorks()
    {
        return new List<LiteraryWork>(_works);
    }
}
