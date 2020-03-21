using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<GameObject> GameObjectsCraft;
    [SerializeField]
    private List<Text> TexteObjets;
    [SerializeField]
    private Canvas CraftCanvas;
    [SerializeField]
    private RectTransform FlecheTransform;
    private List<ObjetCraftable> ObjetsCraftables;
    private int NavigationIndex;
    private bool ObjetCrafte;
    private bool ObjetPlace;
    void Start()
    {
        ObjetPlace = false;
        ObjetsCraftables = new List<ObjetCraftable>();
        ObjetsCraftables.Add(new ObjetCraftable("Abri", "Amazonie_Abri", GameObjectsCraft[0], TexteObjets[0], TexteObjets[0].gameObject.GetComponentInChildren<RawImage>(), new Dictionary<string, int>()
            {
                {"Liane", 5},
                {"Bois", 10 }
            }));

        ObjetsCraftables.Add(new ObjetCraftable("Piege", "Amazonie_Piege", GameObjectsCraft[1], TexteObjets[1], TexteObjets[1].gameObject.GetComponentInChildren<RawImage>(), new Dictionary<string, int>()
            {
                {"Bois", 5 }
            }));

        NavigationIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CraftCanvas.enabled)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && NavigationIndex < GameObjectsCraft.Count - 1)
            {
                NavigationIndex++;
                FlecheTransform.Translate(new Vector3(0, ObjetsCraftables[NavigationIndex].TexteObjet.transform.position.y - ObjetsCraftables[NavigationIndex - 1].TexteObjet.transform.position.y));
                ObjetsCraftables[NavigationIndex].ImageRecette.enabled = true;
                ObjetsCraftables[NavigationIndex - 1].ImageRecette.enabled = false;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && NavigationIndex > 0)
            {
                NavigationIndex--;
                FlecheTransform.Translate(new Vector3(0, ObjetsCraftables[NavigationIndex].TexteObjet.transform.position.y - ObjetsCraftables[NavigationIndex + 1].TexteObjet.transform.position.y));
                ObjetsCraftables[NavigationIndex].ImageRecette.enabled = true;
                ObjetsCraftables[NavigationIndex + 1].ImageRecette.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (VerifieIngredients(ObjetsCraftables[NavigationIndex]))
                {
                    RetireIngredients(ObjetsCraftables[NavigationIndex]);
                    ObjetCrafte = true;
                }
            }
        }

        if (ObjetCrafte)
        {
            CraftCanvas.enabled = false;
            PlayerMovement.BloqueMouvement = false;
            if (!ObjetPlace)
            {
                if (mouseLook.ChoixPosition)
                {
                    ObjetsCraftables[NavigationIndex].Objet.transform.position = mouseLook.TerrainRaycastHit.point;
                    if (VerifiePlacement() && Input.GetKey(KeyCode.Space))
                    {
                        ObjetPlace = true;
                        Instantiate(ObjetsCraftables[NavigationIndex].Objet, Vector3.zero, Quaternion.identity);
                        ObjetsCraftables[NavigationIndex].Objet.transform.Find(ObjetsCraftables[NavigationIndex].NomGO + "_MOD").gameObject.SetActive(true);
                        foreach(GameObject socle in ObjetsCraftables[NavigationIndex].Socles)
                        {
                            socle.SetActive(false);
                        }

                        ObjetCrafte = false;
                        ObjetPlace = false;
                    }
                }
            }
        }
    }

    private bool VerifieIngredients(ObjetCraftable objetACrafter)
    {
        bool peutCrafter = true;

        if (Inventaire.listeObjets.Count > 0)
        {
            foreach (KeyValuePair<string, int> ingredient in objetACrafter.Recette)
            {
                if (Inventaire.listeObjets.ContainsKey(ingredient.Key) && peutCrafter == true)
                {
                    if (!(Inventaire.listeObjets[ingredient.Key] >= (ingredient.Value)))
                    {
                        peutCrafter = false;
                    }
                }
                else
                {
                    peutCrafter = false;
                }
            }
        }
        else
        {
            peutCrafter = false;
        }

        return peutCrafter;
    }

    private bool VerifiePlacement()
    {
        bool placementValide = true;

        foreach (GameObject socle in ObjetsCraftables[NavigationIndex].Socles)
        {           
            if (socle.tag == "Non-Collision")
            {
                placementValide = false;
            }           
        }
        
        return placementValide;
    }

    private void RetireIngredients(ObjetCraftable objetACrafter)
    {
        foreach (KeyValuePair<string, int> ingredient in objetACrafter.Recette)
        {
            Inventaire.listeObjets[ingredient.Key] -= ingredient.Value;
        }
    }
}
