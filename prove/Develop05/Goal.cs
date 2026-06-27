using System;

public abstract class Goal
{
    private readonly string _name;
    private readonly string _description;
    private readonly int _points;

    protected Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string Name => _name;
    public string Description => _description;
    protected int Points => _points;

    public virtual bool IsComplete() => false;

    public virtual string GetStatusSymbol() => IsComplete() ? "X" : " ";

    public abstract int RecordEvent(bool completed = true);

    public abstract string GetDetailsString();

    public abstract string GetStringRepresentation();

    public static Goal FromString(string line)
    {
        string[] parts = line.Split("|");
        if (parts.Length < 4)
        {
            return null;
        }

        string type = parts[0];
        string name = parts[1];
        string description = parts[2];

        if (!int.TryParse(parts[3], out int points))
        {
            return null;
        }

        switch (type)
        {
            case "SimpleGoal":
                if (parts.Length < 5 || !bool.TryParse(parts[4], out bool isComplete))
                {
                    return null;
                }
                return new SimpleGoal(name, description, points, isComplete);

            case "EternalGoal":
                return new EternalGoal(name, description, points);

            case "ChecklistGoal":
                if (parts.Length < 6 ||
                    !int.TryParse(parts[4], out int completedCount) ||
                    !int.TryParse(parts[5], out int targetCount))
                {
                    return null;
                }

                if (parts.Length < 7 || !int.TryParse(parts[6], out int bonus))
                {
                    return null;
                }

                return new ChecklistGoal(name, description, points, targetCount, bonus, completedCount);

            case "PenaltyGoal":
                if (parts.Length < 5 || !int.TryParse(parts[4], out int penalty))
                {
                    return null;
                }

                int streak = 0;
                if (parts.Length >= 6)
                {
                    int.TryParse(parts[5], out streak);
                }

                return new PenaltyGoal(name, description, points, penalty, streak);

            default:
                return null;
        }
    }
}
