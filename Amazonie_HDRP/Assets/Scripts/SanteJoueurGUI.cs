using UnityEngine;
using UnityEngine.UI;

/**
 * @class SanteJoueurGUI
 * La classe SanteJoueurGUI permet de faire la relation entre les données de santé du joueur stockées dans SanteJoueur et l'interface de santé du joueur
 * @author Valentin
 */
public class SanteJoueurGUI : MonoBehaviour
{
    /**
     * Objet entier de la jauge de faim en champ sérialisé
     */
    [SerializeField] GameObject JaugeFaim;
    /**
     * Objet entier de la jauge de soif en champ sérialisé
     */
    [SerializeField] GameObject JaugeSoif;
    /**
     * Objet entier de la jauge de sommeil en champ sérialisé
     */
    [SerializeField] GameObject JaugeSommeil;
    /**
     * Image correspondant au niveau de faim du joueur
     */
    [SerializeField] Image ContenuJaugeFaim;
    /**
     * Image correspondant au niveau de soif du joueur
     */
    [SerializeField] Image ContenuJaugeSoif;
    /**
     * Image correspondant au niveau de sommeil du joueur
     */
    [SerializeField] Image ContenuJaugeSommeil;
    /**
     * Coefficient de faim : plus il est elevé, plus la jauge de faim descendra rapidement
     */
    public float CoefficientFaim;
    /**
     * Coefficient de soif : plus il est elevé, plus la jauge de soif descendra rapidement
     */
    public float CoefficientSoif;
    /**
     * Coefficient de sommeil : plus il est elevé, plus la jauge de sommeil descendra rapidement
     */
    public float CoefficientSommeil;

    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        CoefficientFaim = 0.00004f;
        //CoefficientFaim = 0.003f;
        CoefficientSoif = 0.00008f;
        CoefficientSommeil = 0.00004f;
    }

    /**
     * Boucle principale de SanteJoueurGUI
     */
    void Update()
    {
        JaugeFaim.SetActive(!SanteJoueur.Instance.EstMort);
        JaugeSoif.SetActive(!SanteJoueur.Instance.EstMort);
        JaugeSommeil.SetActive(!SanteJoueur.Instance.EstMort);
        if (!SanteJoueur.Instance.EstMort)
        {
            SanteJoueur.Instance.Faim -= CoefficientFaim;
            SanteJoueur.Instance.Soif -= CoefficientSoif;
            SanteJoueur.Instance.Sommeil -= CoefficientSommeil;
        }

        ContenuJaugeFaim.fillAmount = SanteJoueur.Instance.Faim;
        ContenuJaugeSoif.fillAmount = SanteJoueur.Instance.Soif;
        ContenuJaugeSommeil.fillAmount = SanteJoueur.Instance.Sommeil;
    }
}
