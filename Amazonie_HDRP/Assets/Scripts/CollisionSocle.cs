using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSocle : MonoBehaviour
{
    [SerializeField]
    private Material MaterialPositionValide;
    [SerializeField]
    private Material MaterialPositionInterdite;
    private MeshRenderer MeshRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer.material = MaterialPositionValide;
        gameObject.tag = "Collision";
    }

    private void OnTriggerExit(Collider other)
    {
        MeshRenderer.material = MaterialPositionInterdite;
        gameObject.tag = "Non-Collision";
    }
}
