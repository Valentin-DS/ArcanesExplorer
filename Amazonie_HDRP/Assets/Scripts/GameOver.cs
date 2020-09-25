using UnityEngine;

/**
 * @class GameOver
 * La classe GameOver correspond à la gestion des évènements Respawn ou Exit sur l'écran de GameOver
 * @author Valentin
 * */
public class GameOver : MonoBehaviour
{
    /**
     * Transform du joueur en champ sérialisé
     */
    [SerializeField] Transform transformJoueur;


    /**
     * Méthode de respawn du joueur
     */
    public void Respawn()
    {
        SanteJoueur.Instance.Nourriture = Constantes.SANTE_MAX;
        SanteJoueur.Instance.Eau = Constantes.SANTE_MAX;
        SanteJoueur.Instance.Repos = Constantes.SANTE_MAX;
        SanteJoueur.Instance.EstMort = false;
        PlayerMovement.Instance.velocity.y = 0;
        GetComponent<Canvas>().gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerMovement.BloqueMouvement = false;
    }

    /**
     * Méthode permettant de quitter le jeu
     */
    public void Exit()
    {
        Application.Quit();
    }
}
