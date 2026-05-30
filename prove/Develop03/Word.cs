using System.Text;

class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (!_isHidden)
        {
            return _text;
        }

        StringBuilder masked = new StringBuilder();

        foreach (char character in _text)
        {
            if (char.IsLetterOrDigit(character))
            {
                masked.Append('_');
            }
            else
            {
                masked.Append(character);
            }
        }

        return masked.ToString();
    }
}