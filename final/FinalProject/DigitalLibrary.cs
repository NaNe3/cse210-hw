using System;
using System.Collections.Generic;

public class DigitalLibrary
{
    private readonly List<Author> _authors = new List<Author>();
    private readonly List<LiteraryWork> _works = new List<LiteraryWork>();

    public void AddAuthor(Author author)
    {
        foreach (Author existing in _authors)
        {
            if (existing.Name.Equals(author.Name, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        _authors.Add(author);
    }

    public void AddWork(LiteraryWork work)
    {
        if (!_works.Contains(work))
        {
            _works.Add(work);
        }

        AddAuthor(work.GetAuthor());
    }

    public List<LiteraryWork> GetWorks()
    {
        return new List<LiteraryWork>(_works);
    }
}
