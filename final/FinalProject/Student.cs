using System;
using System.Collections.Generic;

public class Student
{
    private readonly List<ProgressRecord> _records = new List<ProgressRecord>();
    private readonly List<StudyRoadmap> _roadmaps = new List<StudyRoadmap>();

    public string Name { get; }

    public Student(string name)
    {
        Name = name;
    }

    public void AddNewWork(LiteraryWork work)
    {
        foreach (ProgressRecord record in _records)
        {
            if (record.Work.Title.Equals(work.Title, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }

        _records.Add(new ProgressRecord(work));
    }

    public bool TrackProgress(string title, int unitsDone)
    {
        foreach (ProgressRecord record in _records)
        {
            if (record.Work.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                record.UpdateProgress(unitsDone);
                return true;
            }
        }

        return false;
    }

    public void AddRoadmap(StudyRoadmap roadmap)
    {
        _roadmaps.Add(roadmap);
    }

    public List<ProgressRecord> GetRecords()
    {
        return new List<ProgressRecord>(_records);
    }

    public List<StudyRoadmap> GetRoadmaps()
    {
        return new List<StudyRoadmap>(_roadmaps);
    }
}
