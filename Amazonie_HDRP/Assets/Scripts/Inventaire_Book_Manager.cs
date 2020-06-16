﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class Inventaire_Book_Manager : MonoBehaviour
{
    public GameObject canvas_Inventaire;

    public Inventaire inventaire_Player;

    public List<GameObject> liste_Ingredient_RawImage = new List<GameObject>();
    public List<GameObject> liste_Outil_RawImage = new List<GameObject>();
    public List<GameObject> liste_Objet_RawImage = new List<GameObject>();

    public GameObject options_Inventaire;

    public List<GameObject> liste_Objet_Instanciable = new List<GameObject>();

    public int objet_Selectionne;
    public string objet_Categorie;

    public void canvas_Inventaire_Active()
    {
        if (canvas_Inventaire.activeInHierarchy)
        {
            options_Inventaire.SetActive(true);
            unload_Ingredient_Inventaire();
            unload_Objet_Inventaire();
            unload_Outil_Inventaire();
            canvas_Inventaire.SetActive(false);
        }
        else
        {
            load_Ingredient_Inventaire();
            load_Outil_Inventaire();
            load_Objet_Inventaire();
            canvas_Inventaire.SetActive(true);
        }
    }

    private void load_Ingredient_Inventaire()
    {
        for(int i = 0; i < inventaire_Player.liste_Ingredient_Inventaire.Count; i++)
        {
            liste_Ingredient_RawImage[i].SetActive(true);
            liste_Ingredient_RawImage[i].GetComponent<RawImage>().texture = inventaire_Player.liste_Ingredient_Inventaire[i].image_Objet;
            liste_Ingredient_RawImage[i].GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(inventaire_Player.liste_Ingredient_Inventaire[i].quantite_Actuelle);
        }
    }

    private void load_Outil_Inventaire()
    {
        for (int i = 0; i < inventaire_Player.liste_Outil_Inventaire.Count; i++)
        {
            liste_Outil_RawImage[i].SetActive(true);
            liste_Outil_RawImage[i].GetComponent<RawImage>().texture = inventaire_Player.liste_Outil_Inventaire[i].image_Objet;
            liste_Outil_RawImage[i].GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(inventaire_Player.liste_Outil_Inventaire[i].quantite_Actuelle);
        }
    }

    private void load_Objet_Inventaire()
    {
        for (int i = 0; i < inventaire_Player.liste_Objet_Inventaire.Count; i++)
        {
            liste_Objet_RawImage[i].SetActive(true);
            liste_Objet_RawImage[i].GetComponent<RawImage>().texture = inventaire_Player.liste_Objet_Inventaire[i].image_Objet;
            liste_Objet_RawImage[i].GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(inventaire_Player.liste_Objet_Inventaire[i].quantite_Actuelle);
        }
    }

    private void unload_Ingredient_Inventaire()
    {
        for(int i = 0; i< liste_Ingredient_RawImage.Count; i++)
        {
            if (liste_Ingredient_RawImage[i].activeInHierarchy)
            {
                liste_Ingredient_RawImage[i].SetActive(false);
            }
        }
    }

    private void unload_Outil_Inventaire()
    {
        for (int i = 0; i < liste_Outil_RawImage.Count; i++)
        {
            if (liste_Outil_RawImage[i].activeInHierarchy)
            {
                liste_Outil_RawImage[i].SetActive(false);
            }
        }
    }

    private void unload_Objet_Inventaire()
    {
        for (int i = 0; i < liste_Objet_RawImage.Count; i++)
        {
            if (liste_Objet_RawImage[i].activeInHierarchy)
            {
                liste_Objet_RawImage[i].SetActive(false);
            }
        }
    }

    public void stockage_Objet_Selectionne(int number_Objet_Inventaire)
    {
        if (!options_Inventaire.activeInHierarchy)
        {
            options_Inventaire.SetActive(true);
        }
        objet_Selectionne = number_Objet_Inventaire;

        Vector3 position_Mouse = Input.mousePosition;
        options_Inventaire.gameObject.transform.position = position_Mouse;
    }
    public void recuperer_Categorie(string categorie)
    {
        objet_Categorie = categorie;
    }
    public void jeter_Objet()
    {
        switch (objet_Categorie)
        {
            case "Ingredient":
                options_Inventaire.SetActive(false);
                inventaire_Player.liste_Ingredient_Inventaire.Remove(inventaire_Player.liste_Ingredient_Inventaire[objet_Selectionne]);
                unload_Ingredient_Inventaire();
                load_Ingredient_Inventaire();
                break;
            case "Objet":
                options_Inventaire.SetActive(false);
                inventaire_Player.liste_Objet_Inventaire.Remove(inventaire_Player.liste_Objet_Inventaire[objet_Selectionne]);
                unload_Objet_Inventaire();
                load_Objet_Inventaire();
                break;
            case "Outil":
                options_Inventaire.SetActive(false);
                inventaire_Player.liste_Outil_Inventaire.Remove(inventaire_Player.liste_Outil_Inventaire[objet_Selectionne]);
                unload_Outil_Inventaire();
                load_Outil_Inventaire();
                break;
        }   
    }
    public void utilisation_Objet()
    {
        //Verifie qu'on utilise bien un objet ou un outil
        if(objet_Categorie == "Ingredient")
        {
            return;
        }
        else
        {
            //Desactive le canvas de craft si l'on peut utiliser l'objet
            canvas_Inventaire_Active();
            string objet_Used = "";

            //On recupere l'objet que l'on veut utiliser dans l'inventaire
            switch (objet_Categorie)
            {
                case "Objet":
                    objet_Used = inventaire_Player.liste_Objet_Inventaire[objet_Selectionne].nom_Objet;
                    break;

                case "Outil":
                    objet_Used = inventaire_Player.liste_Outil_Inventaire[objet_Selectionne].nom_Objet;
                    break;
            }
            //On instancie dans le jeu l'objet choisi pour êter utilisé
            switch (objet_Used)
            {
                //Revoir la position de l'instanciation pour les outils
                case "Hache":
                    Instantiate(liste_Objet_Instanciable[0], Vector3.zero, liste_Objet_Instanciable[0].transform.rotation);
                    break;
                case "Pioche":
                    Instantiate(liste_Objet_Instanciable[1], Vector3.zero, liste_Objet_Instanciable[1].transform.rotation);
                    break;
                case "Canne":
                    Instantiate(liste_Objet_Instanciable[2], Vector3.zero, liste_Objet_Instanciable[2].transform.rotation);
                    break;
                //On instancie les objets à placer sachant qu'ils ont un script qui gère leur positionnement au bout du raycast du joueur
                case "Piege":
                    jeter_Objet();
                    Instantiate(liste_Objet_Instanciable[3], Vector3.zero, liste_Objet_Instanciable[3].transform.rotation);
                    break;
                case "Feu":
                    jeter_Objet();
                    Instantiate(liste_Objet_Instanciable[4], Vector3.zero, liste_Objet_Instanciable[4].transform.rotation);
                    break;
                case "Lit":
                    jeter_Objet();
                    Instantiate(liste_Objet_Instanciable[5], Vector3.zero, liste_Objet_Instanciable[5].transform.rotation);
                    break;
                case "Coffre":
                    jeter_Objet();
                    Instantiate(liste_Objet_Instanciable[6], Vector3.zero, liste_Objet_Instanciable[6].transform.rotation);
                    break;
                case "Gourde":
                    jeter_Objet();
                    Instantiate(liste_Objet_Instanciable[7], Vector3.zero, liste_Objet_Instanciable[7].transform.rotation);
                    break;
            }
        }        
    }


}
