using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionGUI : MonoBehaviour
{
    public static MissionGUI Instance;
    public List<Mission> Missions;
    public Text MissionTitle;
    public List<Text> GoalsText;
    public List<Image> GoalsFrame;
    private Sprite completedGoalSprite;
    private Sprite failedGoalSprite;
    private int missionNumber;
    private float alphaFrameContent;

    public int MissionNumber { get => missionNumber; set => missionNumber = value; }

    private void Start()
    {
        this.completedGoalSprite = Resources.Load<Sprite>("Sprites/Goal_Complete");
        this.failedGoalSprite = Resources.Load<Sprite>("Sprites/Goal_Failure");
        this.missionNumber = 0;
        this.alphaFrameContent = 0;
        this.UpdatePanel();
        Instance = this;
    }

    public void UpdatePanel()
    {
        this.MissionTitle.text = this.Missions[this.missionNumber].Title;
        for (int i = 0; i < 5; i++)
        {
            if (i < this.Missions[this.missionNumber].GoalsName.Count)
            {
                this.GoalsText[i].text = this.Missions[this.missionNumber].GoalsName[i];
                this.DisplayFrame(i, 100);
                if (this.Missions[this.missionNumber].GoalsAchievement[i])
                {
                    this.GoalsFrame[i].transform.GetChild(0).GetComponent<Image>().sprite = this.completedGoalSprite;
                    this.alphaFrameContent = 100;
                }
                else
                {
                    this.alphaFrameContent = 0;
                }

                this.DisplayFrameContent(i, this.alphaFrameContent);

            }
            else
            {
                this.GoalsText[i].text = string.Empty;
                this.DisplayFrame(i, 0);
                this.DisplayFrameContent(i, 0);
            }
        }
    }

    private void DisplayFrame(int index, float alpha)
    {
        this.GoalsFrame[index].color = new Color(this.GoalsFrame[index].color.r, this.GoalsFrame[index].color.g, this.GoalsFrame[index].color.b, alpha);
    }

    private void DisplayFrameContent(int goalIndex, float alpha)
    {
        this.GoalsFrame[goalIndex].transform.GetChild(0).GetComponent<Image>().color = new Color(
            this.GoalsFrame[goalIndex].transform.GetChild(0).GetComponent<Image>().color.r,
            this.GoalsFrame[goalIndex].transform.GetChild(0).GetComponent<Image>().color.g,
            this.GoalsFrame[goalIndex].transform.GetChild(0).GetComponent<Image>().color.b,
            alpha);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && this.missionNumber < this.Missions.Count - 1)
        {
            this.missionNumber++;
            this.UpdatePanel();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) && this.missionNumber > 0)
        {
            this.missionNumber--;
            this.UpdatePanel();
        }
    }
}
