using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public static bool BloqueMouvement;
    public GameObject torchePrefab;
    [SerializeField]
    Canvas craftCanvas;
    private bool activeTorche = false;
    private void Start()
    {
        BloqueMouvement = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!BloqueMouvement)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.E) && mouseLook.hitsmthg)
            {
                Inventaire.ajoutItem(mouseLook.objectHitName.transform.gameObject.tag);
                Destroy(mouseLook.objectHitName.transform.gameObject);
            }

            if (Input.mouseScrollDelta == new Vector2(0, 1))
            {
                activeTorche = !activeTorche;
                torchePrefab.SetActive(activeTorche);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (craftCanvas.enabled == true)
            {
                BloqueMouvement = false;
                craftCanvas.enabled = false;
            }
            else
            {
                BloqueMouvement = true;
                craftCanvas.enabled = true;
            }
        }
    }
}
