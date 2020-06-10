using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Collider : MonoBehaviour
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
        sizeOfCollider = transform.GetComponent<Collider>().bounds.size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BuildingManager.isBuilding && other.tag == "Walled" && pilierScript.isPlaced && !other.GetComponent<Wall>().isSnapped)
        {
            sizeOfWall = other.bounds.size;

            other.GetComponent<Renderer>().material = material_Green;
            Wall wall = other.GetComponent<Wall>();

            wall.isSnapped = true;
            wall.mousePosX = Input.GetAxis("Mouse X");
            wall.mousePosY = Input.GetAxis("Mouse Y");

            float sizeX = 7.19f;
            float sizeZ = 6.7f;

            switch (this.transform.name)
            {
                case "Collider_West":
                    other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, 6.7f);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - sizeZ / 2 + sizeOfCollider.z);
                    break;
                case "Collider_East":
                    other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, 6.7f);
                    other.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + sizeZ / 2 - sizeOfCollider.z);
                    break;
                case "Collider_North":
                    other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, 7.19f);
                    other.transform.position = new Vector3(transform.position.x - sizeX / 2 + sizeOfCollider.x + 0.2332f, transform.position.y, transform.position.z);
                    break;
                case "Collider_South":
                    other.transform.localScale = new Vector3(other.transform.localScale.x, other.transform.localScale.y, 7.19f);
                    other.transform.position = new Vector3(transform.position.x + sizeX / 2 - sizeOfCollider.x - 0.2332f, transform.position.y, transform.position.z);
                    break;
            }
        }
    }
   /* private void OnTriggerExit(Collider other)
    {
        if (BuildingManager.isBuilding && other.tag == "Walled" && pilierScript.isPlaced && !other.GetComponent<Wall>().isSnapped)
        {
            other.GetComponent<Wall>().isSnapped = false;
            other.GetComponent<Renderer>().material = material_Red;
        }
    }*/
}
