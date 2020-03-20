using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPlateForme : MonoBehaviour
{
    private float startTime;
    private float journeyLength;
    public List<Transform> endpoint = new List<Transform>();
    public float speed = 1f;
    private bool activatePlate = false;

    private int index = 0;
    private float distance;

    private void Start()
    {
        
    }
    public void Update()
    {
        if (activatePlate)
        {
            float distCovered = (Time.time - startTime) * speed;

            float fractionOfJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(this.transform.position, endpoint[index].position, fractionOfJourney);
            distance = Vector3.Distance(transform.position, endpoint[index].position);
            if(distance <= 2)
            {
                if (index < endpoint.Count-1)
                {
                    activatePlate = false;
                    loadNextPoint();
                }
                else
                {
                    activatePlate = false;
                    
                }
            }
           
        }
    }
    private void loadNextPoint()
    {
        index++;
        activatePlate = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (index == 0)
        {
            startTime = Time.time;

            journeyLength = Vector3.Distance(this.transform.position, endpoint[index].position);
            activatePlate = true;
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
