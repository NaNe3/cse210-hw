public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent(bool completed = true)
    {
        return Points;
    }

    public override string GetDetailsString()
    {
        return $"[{GetStatusSymbol()}] {Name} ({Description})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{Name}|{Description}|{Points}";
    }
}
