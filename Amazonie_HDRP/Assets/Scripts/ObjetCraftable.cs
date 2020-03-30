using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class ObjetCraftable
 * La classe ObjetCraftable permet de lier plusieurs données au GameObject correspondant à un objet à crafter
 * @author Valentin
 * */
public class ObjetCraftable : MonoBehaviour
{
    /**
     * Nom de l'objet
     */
    public string Nom { get; private set; }
    /**
     * Nom du GameObject de l'objet
     */
    public string NomGO { get; private set; }
    /**
     * GameObject de l'objet
     */
    public GameObject Objet { get; private set; }
    /**
     * Texte de l'objet (visible dans l'interface de craft)
     */
    public Text TexteObjet { get; private set; }
    /**
     * Image de la recette de l'objet (visible dans l'interface de craft)
     */
    public RawImage ImageRecette { get; private set; }
    /**
     * Liste des socles de l'objet permettant de savoir si l'objet est à plat ou non
     */
    public List<GameObject> Socles { get; private set; }
    /**
     * Dictionnaire renseignant les ingrédients nécessaires et leur nombre pour crafter l'objet (visible dans l'interface de craft)
     */
    public Dictionary<string, int> Recette { get; private set; }

    /**
     * Contructeur de l'objet
     * @param nom : Nom de l'objet
     * @param nomGO : Nom du GameObject de l'objet
     * @param objet : GameObject de l'objet
     * @param texteObjet : Texte de l'objet (visible dans l'interface de craft)
     * @param imageRecette : Image de la recette de l'objet (visible dans l'interface de craft)
     * @param recette : Dictionnaire renseignant les ingrédients nécessaires et leur nombre pour crafter l'objet (visible dans l'interface de craft)
     */
    public ObjetCraftable(string nom, string nomGO, GameObject objet, Text texteObjet, RawImage imageRecette, Dictionary<string, int> recette)
    {
        Nom = nom;
        NomGO = nom;
        Objet = objet;
        TexteObjet = texteObjet;
        ImageRecette = imageRecette;
        Recette = recette;
        Socles = new List<GameObject>();
              
        for (int i = 1; i < 5; i++)
        {
            Socles.Add(GameObject.Find(NomGO + "_Socle" + i));
        }        
    }
}
