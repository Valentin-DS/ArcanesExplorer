using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    public bool isPlaced = false;
    public bool isSnapped = false;
    private RaycastHit hit;
    public GameObject cam;
    private Vector3 currentPosition;

    public float mousePosX;
    public float mousePosY;
   // Foundation test;

    public Material material_Foundation;


    private void Start()
    {
        cam = GameObject.Find("Main Camera");
          //test = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPlaced && !isSnapped)
        {
            //maybe a virer
            BuildingManager.isBuilding = true;

            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20))
            {
                if (hit.transform != this.transform)
                {
                    currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    this.transform.position = currentPosition;
                }
            }
        }
        //Si on clique sur le bouton de la souris on pose l'objet
        if (Input.GetMouseButtonDown(0))
        {
            isPlaced = true;
            BuildingManager.object_Actual = null;
            this.GetComponent<Renderer>().material = material_Foundation;
            BuildingManager.isBuilding = false;
            this.enabled = false;
        }
        //Retire le snap si on s'éloigne trop de l'objet
        if (isSnapped && !isPlaced && (Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.2f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.2f))
        {
            isSnapped = false;
        }
    }
}
