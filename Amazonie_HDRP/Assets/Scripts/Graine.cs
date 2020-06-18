using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graine : MonoBehaviour
{
    public GameObject cam;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20))
        {
            if (Input.GetMouseButtonDown(0) && hit.transform.name.Contains("TerreCulti")){
                if(hit.transform.GetComponent<TerreCultivable>().is_Planted == false)
                {
                    hit.transform.GetComponent<TerreCultivable>().is_Planted = true;
                    hit.transform.GetComponent<TerreCultivable>().planted();
                }
                
            }
        }
    }
}
