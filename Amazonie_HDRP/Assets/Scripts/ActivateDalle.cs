using UnityEngine;

/**
 * @class ActivateDalle
 * La classe ActivateDalle correspond à la destruction des dalles dans la 3e salle du temple
 * @author Basile
 */
public class ActivateDalle : MonoBehaviour
{
    /**
     * Numero de la dalle sur laquelle avancer
     */
    public int dalle_numero;
    /**
     * Affiche le signe correspondant à la prochaine dalle
     */
    public AffichageSigne signe;

    /**
     * Détecte la présence du joueur sur la dalle
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        if (this.transform.tag != "activableDalle")
        {
            Invoke("destroyDalle", 0.1f);
        }
        else
        {
            signe.affichage(dalle_numero);
        }
    }

    /**
     * Détruit la dalle courante
     */
    private void destroyDalle()
    {
        this.gameObject.SetActive(false);
    }
}
