using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static bool isBuilding = false;

    public GameObject foundationPrefab;
    public GameObject pilierPrefab;
    public GameObject wallPrefab;

    public static GameObject object_Actual;

    public Material yellow_Color;

    private GameObject[] liste_Affichage_Collider;

    public Inventaire inventaire_Player;

    // Update is called once per frame
    void Update()
    {
        if(object_Actual != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if(object_Actual.name == "Pillier(Clone)")
            {
                liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderPilier");
                foreach (GameObject collider in liste_Affichage_Collider)
                {
                    collider.GetComponent<Renderer>().enabled = false;
                }
            }
            else if(object_Actual.name == "Wall(Clone)")
            {
                liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderWall");
                foreach (GameObject collider in liste_Affichage_Collider)
                {
                    collider.GetComponent<Renderer>().enabled = false;
                }
            }
            Destroy(object_Actual);
            isBuilding = false;
            
        }
    }

    public void craft_Item(string name_Item)
    {
        switch (name_Item)
        {
            case "Fondation":
                isBuilding = true;
                object_Actual = Instantiate(foundationPrefab, Vector3.zero, foundationPrefab.transform.rotation);
                break;
            case "Mur":
                isBuilding = true;
                object_Actual = Instantiate(wallPrefab, Vector3.zero, wallPrefab.transform.rotation);
                liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderWall");
                foreach (GameObject collider in liste_Affichage_Collider)
                {
                    collider.GetComponent<Renderer>().enabled = true;
                    collider.GetComponent<Renderer>().material = yellow_Color;
                }
                break;
            case "Pilier":
                isBuilding = true;
                object_Actual = Instantiate(pilierPrefab, Vector3.zero, pilierPrefab.transform.rotation);
                liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderPilier");
                foreach (GameObject collider in liste_Affichage_Collider)
                {
                    collider.GetComponent<Renderer>().enabled = true;
                    collider.GetComponent<Renderer>().material = yellow_Color;
                }
                break;
            case "Porte":
                break;
            case "Escalier":
                break;
            case "Pioche":
                inventaire_Player.ajout_Outil_Inventaire("Pioche");
                break;
            case "Hache":
                inventaire_Player.ajout_Outil_Inventaire("Hache");
                break;
            case "Canne":
                inventaire_Player.ajout_Outil_Inventaire("Canne");
                break;
            case "Piege":
                inventaire_Player.ajout_Objet_Inventaire("Piege");
                break;
            case "Feu":
                inventaire_Player.ajout_Objet_Inventaire("Feu");
                break;
            case "Lit":
                inventaire_Player.ajout_Objet_Inventaire("Lit");
                break;
            case "Coffre":
                inventaire_Player.ajout_Objet_Inventaire("Coffre");
                break;
            case "Gourde":
                inventaire_Player.ajout_Objet_Inventaire("Gourde");
                break;
            case "Houe":
                inventaire_Player.ajout_Outil_Inventaire("Houe");
                break;
        }
    }
}
