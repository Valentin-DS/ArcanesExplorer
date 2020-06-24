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

    private objet_Craftable objet_Actuel;

    public Inventaire inventaire_Player;

    public GameObject texte_Ingredient_Manquant;
    public List<GameObject> liste_Recette = new List<GameObject>();
    public List<objet_Craftable> liste_Objet_Recette = new List<objet_Craftable>();

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
        bool craft_Possible = false;

        craft_Possible = verif_Ingredient_Dispo(nom_item);

        if (craft_Possible)
        {
            //delete_Ingredient();
            //clear_Inventaire();
            canvas_Craft.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayerMovement.BloqueMouvement = false;
            build.craft_Item(nom_item);
        }
        else
        {
            texte_Ingredient_Manquant.SetActive(true);
            StartCoroutine(disable_Text_Ingredient());
        }
    }

    public bool verif_Ingredient_Dispo(string nom_item)
    {
        bool is_enough = true;
        for(int i = 0; i < liste_Objet_Recette.Count; i++)
        {
            if(nom_item == liste_Objet_Recette[i].nom_Objet)
            {
                objet_Actuel = liste_Objet_Recette[i];
            }
        }
        for(int j = 0; j < objet_Actuel.liste_Ingredient.Count; j++)
        {
            if(number_Ingredient(objet_Actuel.liste_Ingredient[j].nom_Ingredient) < objet_Actuel.liste_Ingredient[j].quantite_necessaire)
            {
                is_enough = false;
            }
        }
        return is_enough;
    }

    public void delete_Ingredient()
    {
        for(int i = 0; i < objet_Actuel.liste_Ingredient.Count; i++)
        {
            for (int j = 0; j < inventaire_Player.liste_Ingredient_Inventaire.Count; j++)
            {
                if(inventaire_Player.liste_Ingredient_Inventaire[j].nom_Objet == objet_Actuel.liste_Ingredient[i].nom_Ingredient)
                {
                    if(inventaire_Player.liste_Ingredient_Inventaire[j].quantite_Actuelle < objet_Actuel.liste_Ingredient[i].quantite_necessaire && objet_Actuel.liste_Ingredient[i].quantite_necessaire != 0)
                    {
                        objet_Actuel.liste_Ingredient[i].quantite_necessaire -= inventaire_Player.liste_Ingredient_Inventaire[j].quantite_Actuelle;
                        inventaire_Player.liste_Ingredient_Inventaire[j].quantite_Actuelle = 0;
                    }
                    else if(objet_Actuel.liste_Ingredient[i].quantite_necessaire != 0 && inventaire_Player.liste_Ingredient_Inventaire[j].quantite_Actuelle >= objet_Actuel.liste_Ingredient[i].quantite_necessaire)
                    {
                        inventaire_Player.liste_Ingredient_Inventaire[j].quantite_Actuelle -= objet_Actuel.liste_Ingredient[i].quantite_necessaire;
                        break;
                    }
                }
            }
        } 
    }

    public void clear_Inventaire()
    {
        inventaire_Player.liste_Ingredient_Inventaire.RemoveAll(objet_Inventaire => objet_Inventaire.quantite_Actuelle == 0);
    }
    public int number_Ingredient(string nom_ingredient)
    {
        int number_Total_Ingredient = 0;
        for(int i = 0; i < inventaire_Player.liste_Ingredient_Inventaire.Count; i++)
        {
            if(inventaire_Player.liste_Ingredient_Inventaire[i].nom_Objet == nom_ingredient)
            {
                number_Total_Ingredient += inventaire_Player.liste_Ingredient_Inventaire[i].quantite_Actuelle;
            }
        }
        return number_Total_Ingredient;
    }
    IEnumerator disable_Text_Ingredient()
    {
        yield return new WaitForSeconds(1f);
        texte_Ingredient_Manquant.SetActive(false);
    }
}

[System.Serializable]
public class objet_Craftable
{
    public string nom_Objet;
    public List<total_Ingredient> liste_Ingredient = new List<total_Ingredient>();
}

[System.Serializable]
public class total_Ingredient
{
    public string nom_Ingredient;
    public int quantite_necessaire;
}