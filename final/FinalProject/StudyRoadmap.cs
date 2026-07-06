using System;
using System.Collections.Generic;

public class StudyRoadmap
{
    private readonly List<Milestone> _milestones = new List<Milestone>();

    public DateTime CreatedDate { get; }

    public StudyRoadmap()
    {
        CreatedDate = DateTime.Today;
    }

    public void AddMilestone(Milestone milestone)
    {
        _milestones.Add(milestone);
    }

    public Milestone NextMilestone()
    {
        Milestone next = null;

        foreach (Milestone milestone in _milestones)
        {
            if (milestone.IsCompleted)
            {
                continue;
            }

            if (next == null || milestone.TargetDate < next.TargetDate)
            {
                next = milestone;
            }
        }

        return next;
    }

    public List<Milestone> GetMilestones()
    {
        return new List<Milestone>(_milestones);
    }
}
