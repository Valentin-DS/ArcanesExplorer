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
            Invoke("destroyDalle", 0.1f);
        }
        else
        {
            signe.affichage(dalle_numero);
        }
    }
    private void destroyDalle()
    {
        this.gameObject.SetActive(false);
    }
}
