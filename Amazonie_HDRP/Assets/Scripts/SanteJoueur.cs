using UnityEditor.Experimental.TerrainAPI;
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
    public float Nourriture;
    /**
     * Soif du joueur
     */
    public float Eau;
    /**
     * Sommeil du joueur
     */
    public float Repos;
    /**
     * Booléen déterminant si le joueur est mort
     */
    public bool EstMort { get; set; }
    /**
     * Couleur courante lors de l'effet fondu de transition vers le GameOver
     * @see Canvas CanvasGameOver
     */
    private Color EffetFonduGameOver;

    public bool ActiveAnimation;

    public Animation AnimationBras;

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
        Nourriture = Constantes.SANTE_MAX;
        Eau = Constantes.SANTE_MAX;
        Repos = Constantes.SANTE_MAX;
        EstMort = false;
        EffetFonduGameOver = new Color();
        Instance = this;
        ActiveAnimation = false;
        this.CanvasPrincipal.transform.Find("Missions").gameObject.SetActive(true);
        CanvasPrincipal.GetComponentInChildren<Image>().color = EffetFonduGameOver;
        EffetFonduGameOver.a = 0f;
    }


    /**
     * Boucle principale de SanteJoueur
     */
    void Update()
    {
        if(Nourriture <= 0 || Eau <= 0 || Repos <= 0)
        {
            this.EstMort = true;
            valideMort();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            activeEnigme();
        }
    }


    private void valideMort()
    {
        if (HUD.Instance.AnimationJouee && AnimationBras.clip == AnimationBras.GetClip(Constantes.BRAS_ANIMATION_ALLER))
        {
            AnimationBras.clip = AnimationBras.GetClip(Constantes.BRAS_ANIMATION_RETOUR);
            AnimationBras.Play();
            HUD.Instance.AnimationJouee = false;
        }

        PlayerMovement.BloqueMouvement = true;
        if (EffetFonduGameOver.a < 1)
        {
            CanvasPrincipal.GetComponentInChildren<Image>().color = EffetFonduGameOver;
            EffetFonduGameOver.a += 0.01f;
        }
        else
        {
            PlayerMovement.ARespawn = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CanvasGameOver.gameObject.SetActive(true);
        }
    }

    private void activeEnigme()
    {
        if (this.CanvasPrincipal.transform.Find("Missions").gameObject.activeInHierarchy)
        {
            this.CanvasPrincipal.transform.Find("Missions").gameObject.SetActive(false);
        }
        else
        {
            this.CanvasPrincipal.transform.Find("Missions").gameObject.SetActive(true);
        }
    }
}
