using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @class Craft
 * La classe Craft permet de régir l'ensemble du système de craft : Renseigne tous les objets disponibles et fait le lien entre les données de l'inventaire du joueur et l'interface de craft
 * @author Valentin
 */
public class Craft : MonoBehaviour
{
    /**
     * Liste des modèles 3D renseignés en champ sérialisé
     */
    [SerializeField]
    private List<GameObject> GameObjectsCraft;
    /**
     * Texte des objets renseignés en champ sérialisé faisant référence à l'interface de craft
     */
    [SerializeField]
    private List<Text> TexteObjets;
    /**
     * Interface de craft
     */
    [SerializeField]
    private Canvas CraftCanvas;
    /**
     * Position de la flèche de choix d'objet à crafter dans l'interface de craft
     */
    [SerializeField]
    private RectTransform FlecheTransform;
    /**
     * Liste d'objets de la classe ObjetCraftable permettant de lier au modèle 3D d'autres données spécifiques
     */
    private List<ObjetCraftable> ObjetsCraftables;
    /**
     * Index permettant de naviguer dans les listes
     * @see List<GameObject> GameObjectsCraft
     * @see List<ObjetCraftable> ObjetsCraftables
     */
    private int NavigationIndex;
    /**
     * Booléen déterminant si l'objet est crafté ou non
     */
    private bool ObjetCrafte;
    /**
     * Booléen déterminant si l'objet est placé ou non
     */
    private bool ObjetPlace;

    /**
     * Initialisation des paramètres
     * @see List<ObjetCraftable> ObjetsCraftables
     * @see int NavigationIndex
     */
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

    /**
     * Boucle principale de Craft : gère la navigation dans l'interface, le choix, le placement, et la validation
     */
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

    /**
     * Méthode permettant de vérifier si le joueur a suffisament d'ingrédients pour crafter l'objet demandé
     * @param objetACrafter : Objet demandé par le joueur
     */
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

    /**
     * Méthode permettant de vérifier si les 4 socles de l'objet touchent bien le sol
     * @see CollisionSocle
     */
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

    /**
     * Méthode permettant de retirer de l'inventaire les ingrédients nécessaires pour crafter l'objet demandé
     * @param objetACrafter : Objet demandé par le joueur
     * @see bool VerifieIngredients(ObjetCraftable objetACrafter)
     */
    private void RetireIngredients(ObjetCraftable objetACrafter)
    {
        foreach (KeyValuePair<string, int> ingredient in objetACrafter.Recette)
        {
            Inventaire.listeObjets[ingredient.Key] -= ingredient.Value;
        }
    }
}
