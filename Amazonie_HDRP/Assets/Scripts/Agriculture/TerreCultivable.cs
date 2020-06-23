using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerreCultivable : MonoBehaviour
{
    public bool is_Planted = false;
    public bool is_Recoltable = false;
    public int day_Of_Plant = 0;
    public Cultivable_Manager manager_Cultivable;

    public GameObject next_Day_Terre;

    int layerMask = 11;
    public void next_Day_Upgrade()
    {
        if(this.day_Of_Plant < 3)
        {
            GameObject new_plant = Instantiate(next_Day_Terre, this.transform.position, Quaternion.identity);
            new_plant.GetComponent<TerreCultivable>().manager_Cultivable = GameObject.Find("Agriculture_Manager").GetComponent<Cultivable_Manager>();
            new_plant.GetComponent<TerreCultivable>().day_Of_Plant = day_Of_Plant + 1;
            new_plant.GetComponent<TerreCultivable>().is_Planted = true;
            manager_Cultivable.liste_Terre_Cultivable_Temporaire.Add(new_plant);
            Destroy(this.gameObject);
        }
        else
        {
            is_Recoltable = true;
            this.gameObject.layer = layerMask;
            this.gameObject.name = "Patate";
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
