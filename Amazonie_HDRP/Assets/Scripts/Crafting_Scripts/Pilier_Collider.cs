using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilier_Collider : MonoBehaviour
{
    public Foundation foundationScript;
    Vector3 sizeOfPilier;

    public Material material_Green;
    public Material material_Red;

    public Vector3 positionFoundation;
    // Start is called before the first frame update
    void Start()
    {
        foundationScript = transform.parent.parent.GetComponent<Foundation>();
        sizeOfPilier = transform.parent.parent.GetComponent<Collider>().bounds.size;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("onTriggerEnter");
        Pilier pilier = other.GetComponent<Pilier>();
        if (BuildingManager.isBuilding && other.tag == "Pilier" && foundationScript.isPlaced && !other.GetComponent<Pilier>().isSnapped)
        {
            Debug.Log("onTriggerEnterIFFFF");
            other.GetComponent<Pilier>().positionFoundation = transform.parent.parent.position;
            other.GetComponent<Renderer>().material = material_Green;
            

            pilier.isSnapped = true;
            pilier.mousePosX = Input.GetAxis("Mouse X");
            pilier.mousePosY = Input.GetAxis("Mouse Y");

            float sizeX = sizeOfPilier.x;
            float sizeZ = sizeOfPilier.z;

            switch (this.transform.name)
            {
                case "Collider_Pillier_H_D":
                    Debug.Log(this.transform.name);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
                case "Collider_Pillier_H_G":
                    Debug.Log(this.transform.name);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
                case "Collider_Pillier_B_D":
                    Debug.Log(this.transform.name);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
                case "Collider_Pillier_B_G":
                    Debug.Log(this.transform.name);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    break;
            }
        }
    }
    /*private void OnTriggerExit(Collider other)
    {
        if (BuildingManager.isBuilding && other.tag == "Pilier" && foundationScript.isPlaced && !other.GetComponent<Pilier>().isSnapped)
        {
            other.GetComponent<Pilier>().isSnapped = false;
            other.GetComponent<Renderer>().material = material_Red;
        }
    }*/
}
