using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportementHache : MonoBehaviour
{
    // Start is called before the first frame update
    Animation animationHache;
    void Start()
    {
        animationHache = GetComponent<Animation>();
        animationHache.clip.legacy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animationHache.Play();
        }
    }
}
