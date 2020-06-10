using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation_Collider : MonoBehaviour
{
    public Foundation foundationScript;
    Vector3 sizeOfFoundation;

    // Start is called before the first frame update
    void Start()
    {
        foundationScript = transform.parent.parent.GetComponent<Foundation>();
        sizeOfFoundation = transform.parent.parent.GetComponent<Collider>().bounds.size;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Le if dépend de l'objet sur lequel on rentre en collision et pas l'objet en lui même :)
        if(BuildingManager.isBuilding && other.tag == "Foundation" && foundationScript.isPlaced && !other.GetComponent<Foundation>().isSnapped)
        {
            Foundation foundation = other.GetComponent<Foundation>();

            foundation.isSnapped = true;
            foundation.mousePosX = Input.GetAxis("Mouse X");
            foundation.mousePosY = Input.GetAxis("Mouse Y");

            float sizeX = sizeOfFoundation.x;
            float sizeZ = sizeOfFoundation.z;

            switch (this.transform.name)
            {
                case "West_Collider":
                    other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, transform.parent.parent.position.y, transform.parent.position.z);
                    break;
                case "East_Collider":
                    other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, transform.parent.parent.position.y, transform.parent.position.z);
                    break;
                case "North_Collider":
                    other.transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.position.z + sizeZ);
                    break;
                case "South_Collider":
                    other.transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y, transform.parent.position.z - sizeZ);
                    break;
            }
        }
    }
}
