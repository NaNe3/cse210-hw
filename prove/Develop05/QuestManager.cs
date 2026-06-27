using System;
using System.Collections.Generic;
using System.IO;

public class QuestManager
{
  private readonly List<Goal> _goals = new List<Goal>();
  private int _score, _level = 1;

  public void Start()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("==== Eternal Quest ====");
      Console.WriteLine($"Score: {_score} | Level: {_level} ({Title(_level)})\n");
      Console.WriteLine("1. Create New Goal\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Event\n6. Quit\n");
      int c = ReadInt("Select a choice from the menu: ", 1, 6);
      Console.WriteLine();
      if (c == 6) return;
      if (c == 1) CreateGoal();
      else if (c == 2) ListGoals();
      else if (c == 3) SaveGoals();
      else if (c == 4) LoadGoals();
      else RecordEvent();
      Console.WriteLine("\nPress Enter to continue...");
      Console.ReadLine();
    }
  }

  private void CreateGoal()
  {
    Console.WriteLine("1. Simple Goal\n2. Eternal Goal\n3. Checklist Goal\n4. Penalty Goal (creative)");
    int t = ReadInt("Which type of goal would you like to create? ", 1, 4);
    string n = ReadString("What is the name of your goal? "), d = ReadString("What is a short description of it? ");
    Goal g = t == 1 
      ? new SimpleGoal(n, d, ReadPositiveInt("How many points for completing this goal? "))
      : t == 2 
        ? new EternalGoal(n, d, ReadPositiveInt("How many points for each recording of this goal? "))
        : t == 3 
          ? new ChecklistGoal(n, d, ReadPositiveInt("How many points for each completion? "), ReadPositiveInt("How many times does it need to be completed? "), ReadPositiveInt("What is the bonus points when fully completed? "))
          : new PenaltyGoal(n, d, ReadPositiveInt("How many points when you successfully do it? "), ReadPositiveInt("How many points should be deducted when missed? "));
    _goals.Add(g);
    Console.WriteLine("Goal created successfully.");
  }

  private void ListGoals()
  {
    if (_goals.Count == 0) { Console.WriteLine("No goals have been created yet."); return; }
    Console.WriteLine("Your goals are:");
    for (int i = 0; i < _goals.Count; i++) Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
  }

  private void RecordEvent()
  {
    if (_goals.Count == 0) { Console.WriteLine("No goals to record. Create one first PLEAAAASE"); return; }
    Console.WriteLine("The goals are: ");
    for (int i = 0; i < _goals.Count; i++) Console.WriteLine($"{i + 1}. {_goals[i].Name}");
    Goal g = _goals[ReadInt("Which goal did you accomplish/check in on? ", 1, _goals.Count) - 1];
    int delta = g.RecordEvent(g is PenaltyGoal ? ReadYesNo("Did you complete this goal today? (y/n): ") : true);
    if (delta == 0 && g is SimpleGoal) { Console.WriteLine("That simple goal is already complete."); return; }
    _score += delta;
    Console.WriteLine(delta >= 0 ? $"You gained {delta} points." : $"You lost {-delta} points.");
    int nl = Math.Max(0, _score) / 1000 + 1;
    if (nl > _level) Console.WriteLine($"Level up! You reached level {nl}: {Title(nl)}");
    _level = nl;
  }

  private void SaveGoals()
  {
    string f = ReadString("What is the filename for the goal file? ");
    try
    {
      using (StreamWriter w = new StreamWriter(f))
      {
        w.WriteLine($"Score|{_score}");
        w.WriteLine($"Level|{_level}");
        foreach (Goal g in _goals) w.WriteLine(g.GetStringRepresentation());
      }
      Console.WriteLine("Goals saved successfully.");
    }
    catch (Exception ex) { Console.WriteLine($"Error saving goals: {ex.Message}"); }
  }

  private void LoadGoals()
  {
    string f = ReadString("What is the filename to load? ");
    if (!File.Exists(f)) { Console.WriteLine("That file does not exist."); return; }
    try
    {
      _goals.Clear(); _score = 0; _level = 1;
      foreach (string line in File.ReadAllLines(f))
      {
        if (line.StartsWith("Score|")) int.TryParse(line.Split("|")[1], out _score);
        else if (line.StartsWith("Level|")) int.TryParse(line.Split("|")[1], out _level);
        else { Goal g = Goal.FromString(line); if (g != null) _goals.Add(g); }
      }
      _level = Math.Max(_level, Math.Max(0, _score) / 1000 + 1);
      Console.WriteLine("Goals loaded successfully.");
    }
    catch (Exception ex) { Console.WriteLine($"Error loading goals: {ex.Message}"); }
  }

  private static string Title(int l) => l >= 15 ? "Precocious Playa" : l >= 10 ? "Social climba" : l >= 7 ? "Average Bloke" : l >= 4 ? "Bigger Chud" : "Lil Chud";

  private static int ReadInt(string p, int min, int max)
  {
    while (true) { Console.Write(p); string s = Console.ReadLine(); if (int.TryParse(s, out int v) && v >= min && v <= max) return v; Console.WriteLine($"Please enter a number between {min} and {max}."); }
  }

  private static int ReadPositiveInt(string p)
  {
    while (true) { Console.Write(p); string s = Console.ReadLine(); if (int.TryParse(s, out int v) && v > 0) return v; Console.WriteLine("Please enter a whole number greater than 0."); }
  }

  private static string ReadString(string p)
  {
    while (true) { Console.Write(p); string s = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(s)) return s.Trim(); Console.WriteLine("Input cannot be blank."); }
  }

  private static bool ReadYesNo(string p)
  {
    while (true)
    {
      Console.Write(p);
      string s = Console.ReadLine();
      if (string.Equals(s, "y", StringComparison.OrdinalIgnoreCase) || string.Equals(s, "yes", StringComparison.OrdinalIgnoreCase)) return true;
      if (string.Equals(s, "n", StringComparison.OrdinalIgnoreCase) || string.Equals(s, "no", StringComparison.OrdinalIgnoreCase)) return false;
      Console.WriteLine("Please enter y or n.");
    }
  }
}
