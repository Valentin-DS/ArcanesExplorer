﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerreCultivable : MonoBehaviour
{
    public bool is_Planted = false;
    public bool is_Recoltable = false;
    public int day_Of_Plant = 0;
    public Cultivable_Manager manager_Cultivable;

    public GameObject next_Day_Terre;


    public void next_Day_Upgrade()
    {
        if(day_Of_Plant < 4)
        {
            day_Of_Plant++;
            for (int i = 0; i < manager_Cultivable.liste_Terre_Cultivable.Count; i++)
            {
                if (this.gameObject.name == manager_Cultivable.liste_Terre_Cultivable[i].name)
                {
                    manager_Cultivable.liste_Terre_Cultivable.Remove(manager_Cultivable.liste_Terre_Cultivable[i]);
                }
            }
            GameObject new_plant = Instantiate(next_Day_Terre, this.transform.position, Quaternion.identity);
            new_plant.GetComponent<TerreCultivable>().is_Planted = true;
            new_plant.GetComponent<TerreCultivable>().day_Of_Plant = this.day_Of_Plant;
            new_plant.GetComponent<TerreCultivable>().planted();
            Destroy(this.gameObject);
        }
        else
        {
            is_Recoltable = true;
        }
    }
    public void planted()
    {
        if (is_Planted)
        {
            manager_Cultivable = GameObject.Find("Agriculture_Manager").GetComponent<Cultivable_Manager>();
            manager_Cultivable.liste_Terre_Cultivable.Add(this.gameObject);
        }
    }
}
