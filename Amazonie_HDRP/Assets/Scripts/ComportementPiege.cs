using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementPiege : MonoBehaviour
{
    public int temps_Avant_Capture;

    // Start is called before the first frame update
    void Start()
    {
        temps_Avant_Capture = Random.Range(10, 15);
        StartCoroutine(capture_Viande(temps_Avant_Capture));
    }

    IEnumerator capture_Viande(int timer)
    {
        yield return new WaitForSeconds(timer);
        this.gameObject.layer = 11;
    }
}
