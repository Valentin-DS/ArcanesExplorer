using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft_Book_Manager : MonoBehaviour
{
    public GameObject canvas_Abris_GUI;
    public GameObject canvas_Outils_GUI;
    public GameObject canvas_Autres_GUI;

    public GameObject canvas_Craft;
    public BuildingManager build;

    public List<GameObject> liste_Recette = new List<GameObject>();
    //Affichage des bons onglets d'objets à craft
    public void canvas_Abri()
    {
        if (!canvas_Abris_GUI.activeInHierarchy)
        {
            canvas_Autres_GUI.SetActive(false);
            canvas_Outils_GUI.SetActive(false);
            canvas_Abris_GUI.SetActive(true);
        }
    }
    public void canvas_Outils()
    {
        if (!canvas_Outils_GUI.activeInHierarchy)
        {
            canvas_Abris_GUI.SetActive(false);
            canvas_Autres_GUI.SetActive(false);
            canvas_Outils_GUI.SetActive(true);
        }
    }
    public void canvas_Autres()
    {
        if (!canvas_Autres_GUI.activeInHierarchy)
        {
            canvas_Outils_GUI.SetActive(false);
            canvas_Abris_GUI.SetActive(false);
            canvas_Autres_GUI.SetActive(true);
        }
    }

    //Affichage de l'objet à craft
    public void afficher_Recette(string nom_Recette)
    {
        for(int i = 0; i < liste_Recette.Count; i++)
        {
            if(nom_Recette != liste_Recette[i].name)
            {
                liste_Recette[i].SetActive(false);
            }
            else
            {
                liste_Recette[i].SetActive(true);
            }
        }
    }
    public void lancer_craft(string nom_item)
    {
        canvas_Craft.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerMovement.BloqueMouvement = false;
        build.craft_Item(nom_item);
    }
}
