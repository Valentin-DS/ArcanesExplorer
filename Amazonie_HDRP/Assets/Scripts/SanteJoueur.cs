using UnityEngine;
using UnityEngine.UI;

/**
 * @class SanteJoueur
 * La classe SanteJoueur permet de conserver les données relatives à la faim, à la soif, ou au sommeil du joueur
 * @author Valentin
 */
public class SanteJoueur : MonoBehaviour
{
    /**
     * Singleton
     */
    public static SanteJoueur Instance { get; private set; }
    /**
     * Faim du joueur
     */
    public float Faim;
    /**
     * Soif du joueur
     */
    public float Soif;
    /**
     * Sommeil du joueur
     */
    public float Sommeil;
    /**
     * Booléen déterminant si le joueur est mort
     */
    public bool EstMort { get; set; }
    /**
     * Couleur courante lors de l'effet fondu de transition vers le GameOver
     * @see Canvas CanvasGameOver
     */
    private Color EffetFonduGameOver;

    /**
     * Interface de GameOver en champ sérialisé
     */
    [SerializeField]
    public Canvas CanvasGameOver;
    /**
     * Canvas principal sur lequel figure les barres de santé
     */
    [SerializeField]
    public Canvas CanvasPrincipal;
    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        Faim = 1;
        Soif = 1;
        Sommeil = 1;
        EstMort = false;
        EffetFonduGameOver = new Color();
        Instance = this;
    }
    /**
     * Boucle principale de SanteJoueur
     */
    void Update()
    {
        if(Faim <= 0 || Soif <= 0 || Sommeil <= 0)
        {
            EstMort = true;
            PlayerMovement.BloqueMouvement = true;
            if (EffetFonduGameOver.a < 1)
            {
                CanvasPrincipal.GetComponentInChildren<Image>().color = EffetFonduGameOver;
                EffetFonduGameOver.a += 0.01f;
            }
            else
            {
                //PlayerMovement.ARespawn = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                CanvasGameOver.gameObject.SetActive(true);
            }
        }
        else
        {
            CanvasPrincipal.GetComponentInChildren<Image>().color = EffetFonduGameOver;
            EffetFonduGameOver.a = 0f;
        }
    }
}
