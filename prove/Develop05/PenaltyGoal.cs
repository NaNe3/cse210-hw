public class PenaltyGoal : Goal
{
    private readonly int _penalty;
    private int _streak;

    public PenaltyGoal(string name, string description, int successPoints, int penalty, int streak = 0)
        : base(name, description, successPoints)
    {
        _penalty = penalty;
        _streak = streak;
    }

    public override int RecordEvent(bool completed = true)
    {
        if (completed)
        {
            _streak++;
            int earned = Points;

            if (_streak % 5 == 0)
            {
                earned += 50;
            }

            return earned;
        }

        _streak = 0;
        return -_penalty;
    }

    public override string GetDetailsString()
    {
        return $"[{GetStatusSymbol()}] {Name} ({Description}) -- Penalty {_penalty}, Current streak {_streak}";
    }

    public override string GetStringRepresentation()
    {
        return $"PenaltyGoal|{Name}|{Description}|{Points}|{_penalty}|{_streak}";
    }
}
