using System;

public class Milestone
{
    public string Description { get; }
    public DateTime TargetDate { get; }
    public bool IsCompleted { get; private set; }

    public Milestone(string description, DateTime targetDate)
    {
        Description = description;
        TargetDate = targetDate;
        IsCompleted = false;
    }

    public void MarkComplete()
    {
        IsCompleted = true;
    }
}
