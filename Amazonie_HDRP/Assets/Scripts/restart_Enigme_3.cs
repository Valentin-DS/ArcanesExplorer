using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart_Enigme_3 : MonoBehaviour
{
    public List<GameObject> liste_Dalles = new List<GameObject>();
    public Transform restart_Pos;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {        
        reload_Dalle(other);
        Debug.Log("te");
        other.GetComponent<Transform>().Translate(restart_Pos.position);
        Debug.Log("ete");
    }
    private void reload_Dalle(Collider other)
    {
        for(int i=0; i<liste_Dalles.Count; i++)
        {
            liste_Dalles[i].SetActive(true);
        }
    }
}
