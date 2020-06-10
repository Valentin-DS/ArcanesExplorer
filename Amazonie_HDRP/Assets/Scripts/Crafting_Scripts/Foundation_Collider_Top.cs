using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation_Collider_Top : MonoBehaviour
{
    public Pilier pilierScript;
    Vector3 sizeOfWall;
    Vector3 sizeOfCollider;
    public Material material_Green;
    public Material material_Red;
    // Start is called before the first frame update
    void Start()
    {
        pilierScript = transform.parent.GetComponent<Pilier>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (BuildingManager.isBuilding && other.tag == "Foundation" && pilierScript.isPlaced && !other.GetComponent<Foundation>().isSnapped)
        {
            Debug.Log(pilierScript.positionFoundation);

            Foundation foundation = other.GetComponent<Foundation>();

            foundation.isSnapped = true;
            foundation.mousePosX = Input.GetAxis("Mouse X");
            foundation.mousePosY = Input.GetAxis("Mouse Y");

            other.GetComponent<Renderer>().material = material_Green;
            other.transform.position = new Vector3(pilierScript.positionFoundation.x, pilierScript.positionFoundation.y + 9, pilierScript.positionFoundation.z);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (BuildingManager.isBuilding && other.tag == "Foundation" && pilierScript.isPlaced && !other.GetComponent<Foundation>().isSnapped)
        {
            other.GetComponent<Foundation>().isSnapped = false;
            other.GetComponent<Renderer>().material = material_Red;
        }
    }
}
