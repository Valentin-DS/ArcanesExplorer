using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanteJoueur : MonoBehaviour
{
    public static SanteJoueur Instance { get; private set; }
    public float Faim { get; set; }
    public float Soif { get; set; }
    public float Sommeil { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Faim = 1;
        Soif = 1;
        Sommeil = 1;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
