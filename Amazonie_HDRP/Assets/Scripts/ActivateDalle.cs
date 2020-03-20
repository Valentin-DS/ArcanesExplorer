using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDalle : MonoBehaviour
{
    public int dalle_numero;

    public AffichageSigne signe;

    private void OnTriggerEnter(Collider other)
    {
        if (this.transform.tag != "activableDalle")
        {
            Invoke("destroyDalle", 1);
        }
        else
        {
            signe.affichage(dalle_numero);
        }
    }
    private void destroyDalle()
    {
        Destroy(this.gameObject);
    }
}
