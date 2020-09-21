using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    private string name;
    private bool isAchieved;
    public string Name { get => name; set => name = value; }
    public bool IsAchieved { get => isAchieved; set => isAchieved = value; }

    public Goal(string name)
    {
        this.name = name;
        this.isAchieved = false;
    }
}
