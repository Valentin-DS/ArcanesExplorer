using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public double tempsCourant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempsCourant += Time.deltaTime;
        Debug.Log(tempsCourant);
    }

    public void Reinitialise()
    {
        tempsCourant = 0;
    }
}
