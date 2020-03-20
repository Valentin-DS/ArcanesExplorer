using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetCraftable : MonoBehaviour
{
    // Start is called before the first frame update
    public string Nom { get; private set; }
    public string NomGO { get; private set; }
    public GameObject Objet { get; private set; }
    public Text TexteObjet { get; private set; }
    public RawImage ImageRecette { get; private set; }
    public List<GameObject> Socles { get; private set; }
    public Dictionary<string, int> Recette { get; private set; }

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
