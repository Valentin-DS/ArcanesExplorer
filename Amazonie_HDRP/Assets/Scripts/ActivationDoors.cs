using UnityEngine;

/**
 * @class ActivationDoors
 * La classe ActivateDoors correspond à l'ouverture des portes via les détecteurs dans le labyrinthe (2e salle du temple)
 * @author Basile
 */
public class ActivationDoors : MonoBehaviour
{
    /**
     * Porte associée au détecteur
     */
    public GameObject porte;
    /**
     * Booléen d'activation ou non de l'animation d'ouverture de la porte
     */
    private bool activation = true;

    /**
     * Détecte la présence du joueur sur la plaque de détection
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        if (activation)
        {
            this.GetComponent<Animation>().Play();
            porte.GetComponent<Animation>().Play();
            activation = false;
            enabled = false;
        }
    }
    
}
