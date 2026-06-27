public class ChecklistGoal : Goal
{
    private readonly int _targetCount;
    private readonly int _bonus;
    private int _completedCount;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus, int completedCount = 0)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
        _completedCount = completedCount;
    }

    public override bool IsComplete() => _completedCount >= _targetCount;

    public override int RecordEvent(bool completed = true)
    {
        if (IsComplete())
        {
            return 0;
        }

        _completedCount++;
        int earned = Points;

        if (_completedCount >= _targetCount)
        {
            earned += _bonus;
        }

        return earned;
    }

    public override string GetDetailsString()
    {
        return $"[{GetStatusSymbol()}] {Name} ({Description}) -- Completed {_completedCount}/{_targetCount} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{Name}|{Description}|{Points}|{_completedCount}|{_targetCount}|{_bonus}";
    }
}
