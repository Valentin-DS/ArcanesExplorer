using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public Laser laserUse;
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;
    public static bool hitsmthg = false;
    public static RaycastHit objectHitName;
    private float startTime;
    private float journeyLength;
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 11;
        int layerMaskTemple = 1 << 12;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask)){
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
            }
            else if(Input.GetKeyDown(KeyCode.R) && hit.transform.name == "Levier")
            {
                if(GameObject.Find("StartLaser(Clone)") == null)
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
