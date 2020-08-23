using UnityEngine;
using UnityEngine.UI;

/**
 * @class SanteJoueurGUI
 * La classe SanteJoueurGUI permet de faire la relation entre les données de santé du joueur stockées dans SanteJoueur et l'interface de santé du joueur
 * @author Valentin
 */
public class SanteJoueurGUI : MonoBehaviour
{
    [SerializeField] Transform fondJaugeNourriture;
    [SerializeField] Transform fondJaugeEau;
    [SerializeField] Transform fondJaugeRepos;

    [SerializeField] Transform contenuJaugeNourriture;
    [SerializeField] Transform contenuJaugeEau;
    [SerializeField] Transform contenuJaugeRepos;

    float coefficientFaim;
    float coefficientSoif;
    float coefficientSommeil;

    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        this.coefficientFaim = 0.00002f;
        this.coefficientSoif = 0.00004f;
        this.coefficientSommeil = 0.00001f;
    }

    /**
     * Boucle principale de SanteJoueurGUI
     */
    void Update()
    {
        if (!SanteJoueur.Instance.EstMort)
        {
            SanteJoueur.Instance.Nourriture -= this.coefficientFaim;
            SanteJoueur.Instance.Eau -= this.coefficientSoif;
            SanteJoueur.Instance.Repos -= this.coefficientSommeil;
        }

        this.contenuJaugeNourriture.gameObject.transform.localScale = new Vector3(0.02f, SanteJoueur.Instance.Nourriture, 0);
        this.contenuJaugeEau.gameObject.transform.localScale = new Vector3(0.02f, SanteJoueur.Instance.Eau, 0);
        this.contenuJaugeRepos.gameObject.transform.localScale = new Vector3(0.02f, SanteJoueur.Instance.Repos, 0);
    }
}
