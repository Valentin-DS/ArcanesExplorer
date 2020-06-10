using UnityEngine;

/**
 * @class PlayerMovement
 * La classe PlayerMovement régit le déplacement du personnage
 * @author Basile
 */
public class PlayerMovement : MonoBehaviour
{
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
    Vector3 velocity;
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
    [SerializeField]
    Canvas craftCanvas;

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
    /**
     * Booléen déterminant si le repositionnement du joueur au point de spawn doit être effectué
     * @see Vector3 SpawnPoint
     */
    public static bool ARespawn;
    /**
     * Initialisation des paramètres
     */
    private void Start()
    {
        ARespawn = false;
        BloqueMouvement = false;
        SpawnPoint = new Vector3(1050, 4, 415);
        BruitagePas = GetComponent<AudioSource>();
        enMouvement = false;
    }
    /**
     * Boucle principale de PlayerMovement
     */
    void Update()
    {
        if (!BloqueMouvement)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            #region Mouvement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            enMouvement = x > 0 || z > 0;
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
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
                Inventaire.ajoutItem(mouseLook.objectHitName.transform.gameObject.tag);
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
            /*if (craftCanvas.enabled == true)
            {
                BloqueMouvement = false;
                craftCanvas.enabled = false;
            }
            else
            {
                BloqueMouvement = true;
                craftCanvas.enabled = true;
            }*/
            if (craft_Livre_Canvas.activeInHierarchy)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                BloqueMouvement = false;
                craft_Livre_Canvas.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                BloqueMouvement = true;
                craft_Livre_Canvas.SetActive(true);
            }
        }
        #endregion

        #region Respawn

        if (ARespawn)
        {
            GetComponent<Transform>().position = SpawnPoint;
            ARespawn = false;
        }

        #endregion
    }
}
