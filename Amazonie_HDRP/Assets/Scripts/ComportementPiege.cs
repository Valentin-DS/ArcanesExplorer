using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementPiege : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    [SerializeField] Transform transformJoueur;
    void Start()
    {
        timer = GetComponent<Timer>();
        timer.Randomise(10, 30);
        Debug.Log(timer.tempsLimite);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
