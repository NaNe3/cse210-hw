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

    public List<LiteraryWork> SearchByAuthor(string authorName)
    {
        List<LiteraryWork> results = new List<LiteraryWork>();

        foreach (LiteraryWork work in _works)
        {
            if (work.GetAuthor().Name.IndexOf(authorName, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                results.Add(work);
            }
        }

        return results;
    }

    public List<LiteraryWork> SearchByGenre(string genre)
    {
        List<LiteraryWork> results = new List<LiteraryWork>();

        foreach (LiteraryWork work in _works)
        {
            string typeName = work.GetType().Name;
            if (typeName.Equals(genre, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(work);
            }
        }

        return results;
    }

    public List<Author> GetAuthors()
    {
        return new List<Author>(_authors);
    }

    public List<LiteraryWork> GetWorks()
    {
        return new List<LiteraryWork>(_works);
    }
}
