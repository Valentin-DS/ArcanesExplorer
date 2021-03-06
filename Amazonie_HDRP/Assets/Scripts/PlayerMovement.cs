﻿using UnityEngine;

/**
 * @class PlayerMovement
 * La classe PlayerMovement régit le déplacement du personnage
 * @author Basile
 */
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    /**
     * Collider du joueur
     */
    public CharacterController controller;
    /**
     * Point de spawn du joueur
     */
    public static Vector3 SpawnPoint;
    /**
     * Vitesse du joueur
     */
    public float speed = 12f;
    /**
     * Gravité exercée sur le joueur
     */
    public float gravity = -9.81f;
    /**
     * Composant Transform positionné au pied du joueur, sur l'objet GroundCheck
     */
    public Transform groundCheck;
    /**
     * Distance au sol
     */
    public float groundDistance = 0.4f;
    /**
     * LayerMask correspondant au sol
     */
    public LayerMask groundMask;
    /**
     * Proprieté velocity du joueur
     */
    public Vector3 velocity;
    /**
     * Booléen déterminant si le joueur touche le sol ou non
     */
    bool isGrounded;
    /**
     * Booléen déterminant si le joueur doit être bloqué ou non
     */
    public static bool BloqueMouvement;
    /**
     * Objet correspondant à la torche du joueur
     */
    public GameObject torchePrefab;
    /**
     * Interface de craft en champ sérialisé
     */

    public GameObject craft_Livre_Canvas;
    /**
     * Bruitage de déplacement du joueur
     */
    private AudioSource BruitagePas;
    /**
     * Booléen déterminant si la torche doit être activée ou non
     */
    private bool activeTorche = false;
    /**
     * Booléen déterminant si le joueur est actuellement en train de se déplacer ou non
     */
    private bool enMouvement;

    private bool triggerDeath;
    /**
     * Booléen déterminant si le repositionnement du joueur au point de spawn doit être effectué
     * @see Vector3 SpawnPoint
     */
    public static bool ARespawn;
    /**
     * Initialisation des paramètres
     */

    public Inventaire inventaire_Player;

    public Inventaire_Book_Manager inventaire_Book;

    private void Start()
    {
        ARespawn = false;
        BloqueMouvement = false;
        SpawnPoint = new Vector3(1050, 4, 415);
        BruitagePas = GetComponent<AudioSource>();
        enMouvement = false;
        Instance = this;
        this.triggerDeath = false;
    }
    /**
     * Boucle principale de PlayerMovement
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            SaveSystem.Save();
        }

        if (!BloqueMouvement)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            #region Mouvement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            enMouvement = x > 0 || z > 0;
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            if (!this.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {            
                if (velocity.y <= -20f)
                {
                    SanteJoueur.Instance.EstMort = true;
                }
            }

            controller.Move(velocity * Time.deltaTime);
            #endregion

            #region Bruitages
            if (!BruitagePas.Equals(null))
            {
                if (enMouvement && BruitagePas.isPlaying == false)
                {
                    BruitagePas.Play();
                }
                else if (!enMouvement)
                {
                    BruitagePas.Stop();
                }
            }
            #endregion

            #region Récupération objets
            if (Input.GetKeyDown(KeyCode.E) && mouseLook.hitsmthg)
            {
                if (mouseLook.objectHitName.transform.gameObject.tag == "Objet")
                {
                    inventaire_Player.ajout_Objet_Inventaire(mouseLook.objectHitName.transform.gameObject.name);
                }
                else if (mouseLook.objectHitName.transform.gameObject.tag == "Ingredient")
                {
                    inventaire_Player.ajout_Ingredient_Inventaire(mouseLook.objectHitName.transform.gameObject.name);
                }

                Destroy(mouseLook.objectHitName.transform.gameObject);
            }
            #endregion

            #region Torche
            if (Input.mouseScrollDelta == new Vector2(0, 1) && gameObject.tag == "Enigme")
            {
                activeTorche = !activeTorche;
                torchePrefab.SetActive(activeTorche);
            }
            #endregion
        }

        #region Canvas
        if (Input.GetKeyDown(KeyCode.C) && gameObject.tag == "Exploration")
        {
            if (craft_Livre_Canvas.activeInHierarchy)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                BloqueMouvement = false;
                craft_Livre_Canvas.SetActive(false);
            }
            else if (!Inventaire_Book_Manager.Instance.canvas_Inventaire.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                BloqueMouvement = true;
                craft_Livre_Canvas.SetActive(true);
            }
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.I) && gameObject.tag == "Exploration")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            inventaire_Book.canvas_Inventaire_Active();
        }
        #region Respawn

        if (ARespawn)
        {
            GetComponent<Transform>().position = SpawnPoint;
            ARespawn = false;
        }

        #endregion
    }
}
