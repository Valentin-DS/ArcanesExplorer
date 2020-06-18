using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivable_Manager : MonoBehaviour
{
    public List<GameObject> liste_Terre_Cultivable = new List<GameObject>();

    public void terre_Jour_Suivant()
    {
        for(int i = 0; i < liste_Terre_Cultivable.Count; i++)
        {
            liste_Terre_Cultivable[i].GetComponent<TerreCultivable>().next_Day_Upgrade();
        }
    }
}
