using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public bool isPlaced = false;
    public bool isSnapped = false;
    private RaycastHit hit;
    public GameObject cam;
    private Vector3 currentPosition;

    public float mousePosX;
    public float mousePosY;
    Wall test;

    public Material material_Wall;

    private GameObject[] liste_Affichage_Collider;

    private int layerColliderWall = 1 << 20; 
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        test = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced && !isSnapped)
        {
            //maybe a virer
            BuildingManager.isBuilding = true;

            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20, layerColliderWall))
            {
                Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward * 20), Color.yellow);
                if (hit.transform != this.transform)
                {
                    currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    this.transform.position = currentPosition;
                }
            }
        }
        //Si on clique sur le bouton de la souris on pose l'objet
        if (Input.GetMouseButtonDown(0) && isSnapped)
        {
            isPlaced = true;
            liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderWall");
            foreach (GameObject collider in liste_Affichage_Collider)
            {
                collider.GetComponent<Renderer>().enabled = false;
            }
            this.GetComponent<Renderer>().material = material_Wall;
            BuildingManager.object_Actual = null;
            BuildingManager.isBuilding = false;
            this.enabled = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(this.transform.rotation.y != 90)
            {
                this.transform.Rotate(0, 90, 0);
            }
            else
            {
                this.transform.Rotate(0, 0, 0);
            }

        }
        //Retire le snap si on s'éloigne trop de l'objet
        if (isSnapped && !isPlaced && (Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.2f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.5f))
        {
            isSnapped = false;
        }
    }
}
