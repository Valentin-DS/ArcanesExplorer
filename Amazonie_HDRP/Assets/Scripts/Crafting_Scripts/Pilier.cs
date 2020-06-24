using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilier : MonoBehaviour
{
    public bool isPlaced = false;
    public bool isSnapped = false;
    private RaycastHit hit;
    public GameObject cam;
    private Vector3 currentPosition;

    public float mousePosX;
    public float mousePosY;
    
    //A supprimer je pense
    Pilier test;

    public Craft_Book_Manager craft_Book;

    public Vector3 positionFoundation;
    public Material material_Pilier;

    private GameObject[] liste_Affichage_Collider;

    private int layerColliderPilier = 1 << 19;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        craft_Book = GameObject.Find("Craft_Canvas_Manager").GetComponent<Craft_Book_Manager>();

        //A supprimer je pense
        test = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaced && !isSnapped)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20, layerColliderPilier))
            {
                if (hit.transform != this.transform)
                {
                    currentPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    this.transform.position = currentPosition;
                }
            }
        }
        //Si on clique sur le bouton de la souris on pose l'objet
        if (Input.GetMouseButtonDown(0) && isSnapped && BuildingManager.isBuilding)
        {
            liste_Affichage_Collider = GameObject.FindGameObjectsWithTag("ColliderPilier");
            foreach (GameObject collider in liste_Affichage_Collider)
            {
                collider.GetComponent<Renderer>().enabled = false;
            }
            isPlaced = true;
            craft_Book.delete_Ingredient();
            craft_Book.clear_Inventaire();
            BuildingManager.object_Actual = null;
            this.GetComponent<Renderer>().material = material_Pilier;
            BuildingManager.isBuilding = false;
            this.enabled = false;
        }
        //Retire le snap si on s'éloigne trop de l'objet
        if (isSnapped && !isPlaced && (Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 2f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 2f))
        {
            Debug.Log(Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")));
            isSnapped = false;
        }
    }
}
