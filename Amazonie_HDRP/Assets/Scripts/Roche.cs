using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roche : MonoBehaviour
{
    public int point_Vie_Roche = 3;

    public Inventaire inventaire_Joueur;

    // Start is called before the first frame update
    void Start()
    {
        point_Vie_Roche = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Pioche" && other.GetComponent<Animation>().isPlaying && point_Vie_Roche > 0)
        {
            point_Vie_Roche--;
            inventaire_Joueur.ajout_Ingredient_Inventaire("Pierre");
            check_Vie_Roche();

        }
    }
    public void check_Vie_Roche()
    {
        if (point_Vie_Roche <= 0 )
        {
            Invoke("reload_Vie_Roche", 10f);
        }
    }
    private void reload_Vie_Roche()
    {
        point_Vie_Roche = 3;
    }
}
