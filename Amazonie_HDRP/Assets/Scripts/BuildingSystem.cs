using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public List<buildObject> Objects = new List<buildObject>();
    public buildObject currentObject;
    private Vector3 currentPosition;
    public Transform currentPreview;
    public Transform cam;
    public RaycastHit hit;
    public LayerMask layer;

    public float offset = 1.0f;
    public float gridSize = 1.0f;

    public bool isBuilding;

    private void Start()
    {
        currentObject = Objects[0];
        changeCurrentBuilding();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (isBuilding)
        {
            startPreview();
        }
    }
    public void changeCurrentBuilding()
    {
        GameObject curPrev = Instantiate(currentObject.preview, currentPosition, Quaternion.identity);
        currentPreview = curPrev.transform;
    }

    public void startPreview()
    {
        if(Physics.Raycast(cam.position, cam.forward, out hit, 10, layer))
        {
            if (hit.transform != this.transform)
            {
                showPreview(hit);
            }
        }
    }

    public void showPreview(RaycastHit hit2)
    {
        currentPosition = hit2.point;
        currentPosition -= Vector3.one * offset;
        currentPosition /= gridSize;
        currentPosition = new Vector3(Mathf.Round(currentPosition.x), Mathf.Round(currentPosition.y), Mathf.Round(currentPosition.z));
        currentPosition *= gridSize;
        currentPosition += Vector3.one * offset;
        currentPreview.position = currentPosition;
    }
}

[SerializeField]
public class buildObject
{
    public string name;
    public GameObject preview;
    public int gold;
}