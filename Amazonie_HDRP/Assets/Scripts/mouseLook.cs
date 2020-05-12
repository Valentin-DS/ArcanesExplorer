
using System.Collections.Generic;
using UnityEngine;

/**
 * @class mouseLook
 * La classe mouseLook permet de gérer les interactions avec la souris (camera, raycast, etc)
 * @author Basile
 * */
public class mouseLook : MonoBehaviour
{
    /**
     * Instance de la classe Laser
     * @see Laser
     */
    public Laser laserUse;
    /**
     * Sensibilité de la souris
     */
    public float mouseSensitivity = 100f;
    /**
     * Composant Transform du joueur
     */
    public Transform playerBody;
    /**
     * Rotation en x de la camera suite au mouvement de la souris
     */
    float xRotation = 0f;
    /**
     * Booléen indiquant si le laser a touché quelque chose ou non
     */
    public static bool hitsmthg = false;
    /**
     * Booléen indiquant si le choix de la position de l'objet crafté est effectué
     * @see Craft
     */
    public static bool ChoixPosition = false;
    /**
     * Nom de l'objet touché par le laser
     */
    public static RaycastHit objectHitName;
    /**
     * RaycastHit concernant le placement d'un objet crafté
     * @see Craft
     */
    public static RaycastHit TerrainRaycastHit;
    /**
     * Pas d'information sur ce champ
     * @todo Utiliser ce champ ou le supprimer
     */
    private float startTime;
    /**
     * Pas d'information sur ce champ
     * @todo Utiliser ce champ ou le supprimer
     */
    private float journeyLength;
    /**
     * Objet correspondant au laser de la première salle du temple
     */
    public GameObject laser;
    private AudioSource BruitageRotationColonne;

   /* public List<buildObjects> Objects = new List<buildObjects>();
    public buildObjects currentObject;
    private Vector3 currentPosition;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding;*/

    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        //currentObject = Objects[0];
        //changeCurrentBuilding();

        startTime = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        BruitageRotationColonne = GetComponent<AudioSource>();
    }

    /**
     * Boucle principale de mouseLook
     */
    void Update()
    {
        int layerMaskRegenFaim = 1 << 14;
        int layerMaskRegenSoif = 1 << 15;
        int layerMaskRegenSommeil = 1 << 16;
        int layerMaskGround = 1 << 10;
        int layerMask = 1 << 11;
        int layerMaskTemple = 1 << 12;

        RaycastHit hit;
        //Regen faim
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, layerMaskRegenFaim) && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SanteJoueur.Instance.Faim = 1;
        }

        //Regen soif
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMaskRegenSoif) && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SanteJoueur.Instance.Soif = 1;
        }

        //Regen sommeil
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5, layerMaskRegenSommeil) && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SanteJoueur.Instance.Sommeil = 1;
            PlayerMovement.SpawnPoint = playerBody.position;
        }

        //Terrain (Craft)

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMaskGround))
        {
            TerrainRaycastHit = hit;
            ChoixPosition = true;
        }

        //Objet à ramasser
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            hitsmthg = true;
            objectHitName = hit;
        }
        else
        {
            hitsmthg = false;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 15, layerMaskTemple))
        {
            if (Input.GetKeyDown(KeyCode.R) && hit.transform.name != "Levier")
            {
                RotationColumn.startRotation(hit.transform.name);
                BruitageRotationColonne.Play();
            }
            else if (Input.GetKeyDown(KeyCode.R) && hit.transform.name == "Levier" && RotationColumn.call_function)
            {
                if (GameObject.Find("StartLaser(Clone)") == null)
                {
                    hit.transform.GetComponent<Animation>().Play();
                    Instantiate(laser);
                }
            }
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

