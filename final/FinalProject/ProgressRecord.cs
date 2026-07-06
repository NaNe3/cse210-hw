using System;

public class ProgressRecord
{
    public LiteraryWork Work { get; }
    public int UnitsDone { get; private set; }
    public DateTime StartDate { get; }
    public string Status { get; private set; }

    public ProgressRecord(LiteraryWork work)
    {
        Work = work;
        UnitsDone = 0;
        StartDate = DateTime.Today;
        Status = "Not Started";
    }

    public void UpdateProgress(int additionalUnits)
    {
        if (additionalUnits < 0)
        {
            return;
        }

        UnitsDone += additionalUnits;
        if (UnitsDone >= Work.TotalUnits)
        {
            UnitsDone = Work.TotalUnits;
            Status = "Completed";
            return;
        }

        Status = UnitsDone == 0 ? "Not Started" : "In Progress";
    }

    public double PercentComplete()
    {
        if (Work.TotalUnits <= 0)
        {
            return 0;
        }

        return (double)UnitsDone / Work.TotalUnits * 100;
    }

    public string GetDisplayLine()
    {
        return $"{Work.Title}: {UnitsDone}/{Work.TotalUnits} units ({PercentComplete():0.0}%) - {Status} (started {StartDate:yyyy-MM-dd})";
    }
}
