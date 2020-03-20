using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawLaser : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject firePoint;

    private GameObject spawnedLaser;

    public GameObject test;

    public int maxReflectionCount = 5;
    public float maxStepDistance = 200;

    private bool startMovement = false;
    private int current = 0;
    public static List<Vector3> listePosition = new List<Vector3>();

    private float startTime;
    private float journeyLength;
    public float speed = 3f;

    private Vector3 startPoint;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        //Instantiation du laser
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform) as GameObject;
        DrawPredictedReflectionPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
        startPoint = test.transform.position;
        //On coupe le laser
        //DisableLaser();
    }

    private void Update()
    {
        //Si on appuie sur T, le rayon s'active
        if (Input.GetKeyDown(KeyCode.T))
        {
            EnableLaser();
        }
        //Si on reste appuyer le rayon continue de tirer
        if (Input.GetKey(KeyCode.T))
        {
            UpdateLaser();
        }

        //Sinon il se coupe
        /*else
        {
            DisableLaser();
        }*/
        if (startMovement)
        {
            
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            test.transform.position = Vector3.Lerp(startPoint, listePosition[current], fractionOfJourney);
            //lineRebound.SetPosition(current+1, test.transform.localPosition/2);
            if(Vector3.Distance(test.transform.position, listePosition[current]) < 0.5)
            {
                current++;
                startPoint = listePosition[current - 1];
                //lineRebound.positionCount += 1;
                if (current >= listePosition.Count)
                {
                    startMovement = false;
                    spawnedLaser.GetComponent<MovingLaser>().enabled = true;
                }
                else
                {
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(startPoint, listePosition[current]);
                }
                
            }
            
            
        }
    }

    //Active le laser
    void EnableLaser()
    {
        spawnedLaser.SetActive(true);
    }

    //Tire le laser
    void UpdateLaser()
    {
       if(firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }

    }

    //Couper le laser
    void DisableLaser()
    {
        spawnedLaser.SetActive(false);
    }

    //Gestion des rebonds
    private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
    {
        //Condition de sortie selon le nombre de rebonds maximum
        if(reflectionsRemaining == 0)
        {
            journeyLength = Vector3.Distance(test.transform.position, listePosition[current]);
            startMovement = true;
            return;
        }

        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;

        //Si on rentre en collision avec un objet
        if(Physics.Raycast(ray, out hit, maxStepDistance))
        {
            //On récupère la nouvelle direction en fonction du vecteur normal de l'objet récupéré
            direction = Vector3.Reflect(direction, hit.normal);

            //On récupére la position de l'objet touché
            position = hit.point;
        }

        else
        {
            position += direction * maxStepDistance;
            
        }

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(startingPosition, position);
        listePosition.Add(position);
        DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
    }
}
