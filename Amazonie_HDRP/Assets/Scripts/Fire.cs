using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public bool isPlaced = false;
    private RaycastHit hit;
    public GameObject cam;
    private Vector3 currentPosition;
    private int layer_Terrain = 1 << 10;
    public Craft_Book_Manager craft_Book;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        craft_Book = GameObject.Find("Craft_Canvas_Manager").GetComponent<Craft_Book_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20, layer_Terrain))
            {
                if (hit.transform != this.transform)
                {
                    currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    this.transform.position = currentPosition;
                }
            }
        }
        if(Input.GetMouseButtonDown(0) && BuildingManager.isBuilding)
        {
            isPlaced = true;
            craft_Book.delete_Ingredient();
            craft_Book.clear_Inventaire();
            BuildingManager.object_Actual = null;
            BuildingManager.isBuilding = false;
            this.enabled = false;
        }
    }
}
