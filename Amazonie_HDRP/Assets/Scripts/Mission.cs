using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : ScriptableObject
{
    public string Title;
    public List<string> GoalsName;
    public List<bool> GoalsAchievement;
    public bool IsCompleted;
}
