using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDalleEnigme : MonoBehaviour
{
    public Enigme1 enigmeManager;
    public int num;

    private void OnTriggerEnter(Collider other)
    {
        enigmeManager.verifCombinaison(num);

    }
}
