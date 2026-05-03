using System;
using System.Collections.Generic;

public class Resume
{
    public string _fullName = "";
    public List<Job> _jobs = new List<Job>();

    public Resume()
    {
    }

    public void Display()
    {
        Console.WriteLine($"Name: {_fullName}");
        Console.WriteLine("Jobs: ");
        
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}