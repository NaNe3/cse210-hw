using System;
using System.Collections.Generic;

class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _random = new Random();
        _words = ParseWords(text);
    }

    public static Scripture CreateDoctrineAndCovenants4(int startVerse, int endVerse)
    {
        Reference reference = startVerse == endVerse
            ? new Reference("Doctrine & Covenants", 4, startVerse)
            : new Reference("Doctrine & Covenants", 4, startVerse, endVerse);

        string text = GetSelectedVerses(startVerse, endVerse);
        return new Scripture(reference, text);
    }

    public string DisplayVerse()
    {
        List<string> displayWords = new List<string>();

        foreach (Word word in _words)
        {
            displayWords.Add(word.GetDisplayText());
        }

        return $"{_reference.GetDisplayText()}\n\n{string.Join(" ", displayWords)}";
    }

    public void HideRandomWords()
    {
        int wordsToHide = 3;
        List<int> unhiddenIndices = new List<int>();

        for (int index = 0; index < _words.Count; index++)
        {
            if (!_words[index].IsHidden())
            {
                unhiddenIndices.Add(index);
            }
        }

        if (unhiddenIndices.Count == 0)
        {
            return;
        }

        int hideCount = Math.Min(wordsToHide, unhiddenIndices.Count);

        for (int count = 0; count < hideCount; count++)
        {
            int randomPosition = _random.Next(unhiddenIndices.Count);
            int wordIndex = unhiddenIndices[randomPosition];
            _words[wordIndex].Hide();
            unhiddenIndices.RemoveAt(randomPosition);
        }
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }

        return true;
    }

    private List<Word> ParseWords(string text)
    {
        List<Word> words = new List<Word>();
        string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string part in parts)
        {
            words.Add(new Word(part));
        }

        return words;
    }

    private static string GetSelectedVerses(int startVerse, int endVerse)
    {
        List<string> verses = new List<string>();

        for (int verse = startVerse; verse <= endVerse; verse++)
        {
            verses.Add(GetVerseText(verse));
        }

        return string.Join(" ", verses);
    }

    private static string GetVerseText(int verse)
    {
        switch (verse)
        {
            case 1:
                return "Now behold, a marvelous work is about to come forth among the children of men.";
            case 2:
                return "Therefore, O ye that embark in the service of God, see that ye serve him with all your heart, might, mind and strength, that ye may stand blameless before God at the last day.";
            case 3:
                return "Therefore, if ye have desires to serve God ye are called to the work;";
            case 4:
                return "For behold the field is white already to harvest; and lo, he that thrusteth in his sickle with his might, the same layeth up in store that he perisheth not, but bringeth salvation to his soul;";
            case 5:
                return "And faith, hope, charity and love, with an eye single to the glory of God, qualify him for the work.";
            case 6:
                return "Remember faith, virtue, knowledge, temperance, patience, brotherly kindness, godliness, charity, humility, diligence.";
            default:
                return "Ask, and ye shall receive; knock, and it shall be opened unto you. Amen.";
        }
    }
}