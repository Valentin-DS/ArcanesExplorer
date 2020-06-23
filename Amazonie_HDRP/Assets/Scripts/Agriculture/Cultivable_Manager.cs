using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultivable_Manager : MonoBehaviour
{
    public List<GameObject> liste_Terre_Cultivable = new List<GameObject>();

    public List<GameObject> liste_Terre_Cultivable_Temporaire = new List<GameObject>();

    public void terre_Jour_Suivant()
    {
        
        for(int i = 0; i < liste_Terre_Cultivable.Count; i++)
        {
            liste_Terre_Cultivable[i].GetComponent<TerreCultivable>().next_Day_Upgrade();
        }
        liste_Terre_Cultivable.Clear();
        load_New_List();
        //clear_Liste_Terre();
    }
    public void clear_Liste_Terre()
    {
        for (int i = 0; i < liste_Terre_Cultivable.Count; i++)
        {
            if (liste_Terre_Cultivable[i] == null)
            {
                liste_Terre_Cultivable.Remove(liste_Terre_Cultivable[i]);
            }
        }
    }
    public void load_New_List()
    {
        for(int i = 0; i < liste_Terre_Cultivable_Temporaire.Count; i++)
        {
            liste_Terre_Cultivable.Add(liste_Terre_Cultivable_Temporaire[i]);
        }
        liste_Terre_Cultivable_Temporaire.Clear();
    }
}
