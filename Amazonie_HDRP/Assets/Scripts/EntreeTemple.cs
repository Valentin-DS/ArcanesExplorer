using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @class EntreeTemple
 * La classe EntreeTemple correspond à la détection du joueur sur le trigger du temple et son chargement
 * @author Valentin
 * */
public class EntreeTemple : MonoBehaviour
{
    /**
     * Collider du joueur en champ sérialisé
     */
    [SerializeField] private CharacterController collisionJoueur;
    /**
     * Nom de la scène à charger en champ sérialisé
     */
    [SerializeField] private string sceneACharger;
    /**
     * Booléen indiquant si le joueur se trouve dans le trigger du chargement de la scène du temple
     */
    private bool estDansLaZone;

    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        estDansLaZone = false;
    }

    /**
     * Détecte la présence du joueur sur le trigger du temple
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(collisionJoueur))
        {
            estDansLaZone = true;
        }
    }

    /**
     * Détecte la sortie du joueur du trigger du temple
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(collisionJoueur))
        {
            estDansLaZone = false;
        }
    }

    /**
     * Boucle principale d'EntreeTemple
     */
    void Update()
    {
        if(estDansLaZone && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(sceneACharger);
        }
    }
}
