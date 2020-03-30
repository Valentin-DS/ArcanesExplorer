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
        SanteJoueur.Instance.Faim = 1;
        SanteJoueur.Instance.Soif = 1;
        SanteJoueur.Instance.Sommeil = 1;
        SanteJoueur.Instance.EstMort = false;
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
