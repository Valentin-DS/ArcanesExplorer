using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gourde : MonoBehaviour
{
    public int remplissageActuel = 0;
    public int durabilite = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && remplissageActuel >= 0)
        {
            SanteJoueur.Instance.Eau += 0.03f;
            remplissageActuel--;
            durabilite--;
            checkDurabilite();
        }
    }

    private void checkDurabilite()
    {
        if(durabilite == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
