using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * @class Inventaire
 * La classe Inventaire permet de gérer l'inventaire, de faire le lien entre les données et l'interface d'inventaire
 * @author Basile
 * */
public class Inventaire : MonoBehaviour
{
    /**
     * Dictionnaire liant un objet à sa quantité
     */
    public static Dictionary<string, int> listeObjets = new Dictionary<string, int>();
   /**
    * Nombre d'objets maximum
    */
    public static int nbObjetMax = 4;
    /**
     * Nombre d'objets actuels
     */
    public static int nbObjetsActuels = 0;
    /**
     * Canvas pour afficher l'inventaire (en champ serialisé)
     */
    [SerializeField] GameObject inventaireCanvas;
    /**
     * Liste des objets Texte affichant le nombre d'objets (en champ sérialisé)
     */
    [SerializeField] List<TextMeshProUGUI> listeTextSlot;
    /**
     * Ajoute l'objet cliqué à l'inventaire
     * @param Nom de l'objet à ajouter
     */
    public static void ajoutItem(string nomObjet)
    {
        //Vérifie si l'objet n'est pas déjà dans l'inventaire
        if (!listeObjets.ContainsKey(nomObjet))
        {
            //Vérifie s'il reste des slots libres
            if (nbObjetsActuels < nbObjetMax)
            {
                Debug.Log("Vous venez d'ajouter un nouvel item à votre inventaire : " + nomObjet);
                nbObjetsActuels += 1;
                listeObjets.Add(nomObjet, 1);
            }
            //Sinon l'objet est perdu car l'inventaire est plein
            else
            {
                Debug.Log("Votre inventaire est plein");
            }
        }
        //Incrémente la quantité de l'objet s'il fait déjà parti de l'inventaire
        else
        {
            Debug.Log("Vous venez d'augmenter votre quantité de : " + nomObjet + " de 1 ");
            listeObjets[nomObjet] += 1;
        }
    }
    /**
     * Boucle principale d'Inventaire
     */
    private void Update()
    {
        //Vérifie quand le joueur ouvre l'inventaire
        if (Input.GetKeyDown(KeyCode.I) && gameObject.tag == "Exploration")
        {
            enableUI();
        }
    }
    /**
     * Methode gérant l'ouverture de l'inventaire avec le Canvas 
     */
    public void enableUI()
    {
        if (inventaireCanvas.activeSelf)
        {
            inventaireCanvas.SetActive(false);
        }
        else
        {
            inventaireCanvas.SetActive(true);
            loadingInventaire();

        }
    }  
    /**
     * Méthode permettant de charger l'inventaire
     */
    public void loadingInventaire()
    {
        for(int j=0;j<nbObjetsActuels; j++)
        {
            //Si le joueur dispose dans son inventaire de l'objet affiché dans ce slot
            if (listeObjets.ContainsKey(listeTextSlot[j].name)){
                Debug.Log("test :" + listeObjets[listeTextSlot[j].name].ToString());

                //On actualise la quantité selon l'inventaire
                listeTextSlot[j].text = listeObjets[listeTextSlot[j].name].ToString();
            }
        }
    }
}
