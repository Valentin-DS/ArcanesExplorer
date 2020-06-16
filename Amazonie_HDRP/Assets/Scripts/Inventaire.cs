using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
/**
* @class Inventaire
* La classe Inventaire permet de gérer l'inventaire, de faire le lien entre les données et l'interface d'inventaire
* @author Basile
* */
public class Inventaire : MonoBehaviour
{
    public List<Texture> liste_Sprite = new List<Texture>();

    public List<objet_Inventaire> liste_Ingredient_Inventaire = new List<objet_Inventaire>();
    public List<objet_Inventaire> liste_Objet_Inventaire = new List<objet_Inventaire>();
    public List<objet_Inventaire> liste_Outil_Inventaire = new List<objet_Inventaire>();

    private int taille_Ingredient_Inventaire_Max = 50;
    private int taille_Objet_Inventaire_Max = 20;
    private int taille_Outil_Inventaire_Max = 20;

    public GameObject texte_Surcharge;

    public void ajout_Ingredient_Inventaire(string name_Inventaire)
    {
        bool ajout = false;

        //On parcours la liste des ingrédients 
        for(int i = 0; i < liste_Ingredient_Inventaire.Count; i++)
        {
            //On vérifie si l'ingrédient récupéré fait déjà partie de l'inventaire
            if(liste_Ingredient_Inventaire[i].nom_Objet == name_Inventaire)
            {
                //Et on vérifie que ça quantité maximale ne soit pas atteinte
                if(liste_Ingredient_Inventaire[i].quantite_Actuelle < liste_Ingredient_Inventaire[i].quantite_Maximal)
                {
                    liste_Ingredient_Inventaire[i].quantite_Actuelle++;
                    ajout = true;
                    break;
                }   
            }
        }
        //S'il reste de la place dans l'inventaire et que chaque objet identique a atteint sa capacité maximale alors on recréer une case dans l'inventaire
        if(ajout == false && liste_Ingredient_Inventaire.Count < taille_Ingredient_Inventaire_Max)
        {
            objet_Inventaire item = new objet_Inventaire();
            item.nom_Objet = name_Inventaire;
            item.quantite_Actuelle = 1;
            item.quantite_Maximal = 5;
            item.image_Objet = insert_Sprite(name_Inventaire);
            liste_Ingredient_Inventaire.Add(item);
        }
        else if(ajout == false && liste_Ingredient_Inventaire.Count == taille_Ingredient_Inventaire_Max)
        {
            texte_Surcharge.SetActive(false);
            texte_Surcharge.SetActive(true);
            StartCoroutine(disable_Text_Overload());
        }
    }

    public void ajout_Objet_Inventaire(string name_Object)
    {
        bool ajout = false;
        for (int i = 0; i < liste_Objet_Inventaire.Count; i++)
        {
            if (liste_Objet_Inventaire[i].nom_Objet == name_Object)
            {
                if (liste_Objet_Inventaire[i].quantite_Actuelle < liste_Objet_Inventaire[i].quantite_Maximal)
                {
                    liste_Objet_Inventaire[i].quantite_Actuelle++;
                    ajout = true;
                    break;
                }
            }
        }
        if (ajout == false && liste_Objet_Inventaire.Count < taille_Objet_Inventaire_Max)
        {
            objet_Inventaire item = new objet_Inventaire();
            item.nom_Objet = name_Object;
            item.quantite_Actuelle = 1;
            item.quantite_Maximal = 1;
            item.image_Objet = insert_Sprite(name_Object);
            liste_Objet_Inventaire.Add(item);
        }
        else if (ajout == false && liste_Objet_Inventaire.Count == taille_Objet_Inventaire_Max)
        {
            texte_Surcharge.SetActive(false);
            texte_Surcharge.SetActive(true);
            StartCoroutine(disable_Text_Overload());
        }
    }

    public void ajout_Outil_Inventaire(string name_Tools)
    {
        bool ajout = false;
        for (int i = 0; i < liste_Outil_Inventaire.Count; i++)
        {
            if (liste_Outil_Inventaire[i].nom_Objet == name_Tools)
            {
                if (liste_Outil_Inventaire[i].quantite_Actuelle < liste_Outil_Inventaire[i].quantite_Maximal)
                {
                    liste_Outil_Inventaire[i].quantite_Actuelle++;
                    ajout = true;
                    break;
                }
            }
        }
        if (ajout == false && liste_Outil_Inventaire.Count < taille_Outil_Inventaire_Max)
        {
            objet_Inventaire item = new objet_Inventaire();
            item.nom_Objet = name_Tools;
            item.quantite_Actuelle = 1;
            item.quantite_Maximal = 1;
            item.image_Objet = insert_Sprite(name_Tools);
            liste_Outil_Inventaire.Add(item);
        }
        else if (ajout == false && liste_Outil_Inventaire.Count == taille_Outil_Inventaire_Max)
        {
            texte_Surcharge.SetActive(false);
            texte_Surcharge.SetActive(true);
            StartCoroutine(disable_Text_Overload());
        }
    }

    public Texture insert_Sprite(string name_Object)
    {
        Texture sprite_Finale = null;
        switch (name_Object)
        {
            case "Bois":
                sprite_Finale = liste_Sprite[0];
                break;
            case "Feuille":
                sprite_Finale = liste_Sprite[1];
                break;
            case "Liane":
                sprite_Finale = liste_Sprite[2];
                break;
        }
        return sprite_Finale;
    }
    IEnumerator disable_Text_Overload()
    {
        yield return new WaitForSeconds(1f);
        texte_Surcharge.SetActive(false);
    }
}

[System.Serializable]
public class objet_Inventaire
{
    public string nom_Objet;
    public int quantite_Actuelle;
    public int quantite_Maximal;
    public Texture image_Objet = null;

}