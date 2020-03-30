using System.Collections.Generic;
using UnityEngine;

/**
 * @class AffichageSigne
 * La classe AffichageSigne correspond à l'affichage du signe indiquant au joueur comment se déplacer à la 3e salle du temple
 * @author Basile
 */
public class AffichageSigne : MonoBehaviour
{
    /**
     * Objet sur lequel est affiché le signe
     */
    public GameObject plane_Obj;
    /**
     * Liste des signes à afficher
     */
    public List<Material> liste_Mat = new List<Material>();

    /**
     * Méthode d'affichage du signe
     * @param Index correspondant à un signe à afficher dans la liste
     * @see List<Material> liste_Mat
     */
    public void affichage(int num)
    {
        plane_Obj.GetComponent<Renderer>().material = liste_Mat[num];
    }
}
