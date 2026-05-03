using System;

class Program
{
    static void Main(string[] args)
    {
        Job firstJob = new Job();
        firstJob._jobName = "Software Engineer";
        firstJob._companyName = "Microsoft";
        firstJob._startYear = 2019;
        firstJob._endYear = 2022;

        Job secondJob = new Job();
        secondJob._jobName = "Manager";
        secondJob._companyName = "Apple";
        secondJob._startYear = 2022;
        secondJob._endYear = 2023;

        Resume myResume = new Resume();
        myResume._fullName = "Allison Rose";
        myResume._jobs.Add(firstJob);
        myResume._jobs.Add(secondJob);

        myResume.Display();
    }
}