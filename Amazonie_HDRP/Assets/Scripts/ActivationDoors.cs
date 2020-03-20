using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDoors : MonoBehaviour
{
    public GameObject porte;
    private bool activation = true;
    private bool activatePlate = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (activation)
        {
            this.GetComponent<Animation>().Play();
            porte.GetComponent<Animation>().Play();
            activation = false;
            enabled = false;
        }
    }
    
}
