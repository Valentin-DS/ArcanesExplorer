using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int maxReflectionCount = 5;
    public float maxStepDistance = 25;

    public GameObject porte;
    private bool launchingDraw = false;
    public List<Vector3> test = new List<Vector3>();

    public Vector3 finishLaser = new Vector3(-5.44f, 8.29f, 11.3f);
    private Vector3 origin;
    private Vector3 target;

    private LineRenderer lineRenderer;

    private float dist;
    private float counter;

    public float lineDrawSpeed = 6f;
    private int wayPointIndex = 0;
    private float nextPoint;

    // Start is called before the first frame update
    void Start()
    {
        porte = GameObject.Find("Temple_Porte");
        lineRenderer = GetComponent<LineRenderer>();
        test.Add(this.transform.position);
        lineRenderer.SetPosition(0, test[0]);
        DrawPredictedReflectionsPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (launchingDraw)
        {
            counter += 0.1f / lineDrawSpeed * Time.deltaTime;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = origin;
            Vector3 pointB = target;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
            for(int i =wayPointIndex; i < test.Count - 1; i++)
            {
                lineRenderer.SetPosition(i + 1, pointAlongLine);
            }
            

            nextPoint = Vector3.Distance(pointB, pointAlongLine);

            if(nextPoint < 0.2f)
            {
                if(wayPointIndex < lineRenderer.positionCount - 2)
                {
                    GetNextWayPoint();
                }
                else
                {
                    float distanceFinal = Vector3.Distance(test[wayPointIndex+1], finishLaser);
                    if (distanceFinal < 1.5f)
                    {
                        Debug.Log("Vous avez gagné : Lancer l'animation");
                        porte.GetComponent<Animation>().Play();
                    }
                    else
                    {
                        Invoke("endLaser", 2);
                        
                    }
                    enabled = false;
                }
            }
        }   
    }

    void DrawPredictedReflectionsPattern(Vector3 position, Vector3 direction, int reflectionRemaining)
    {
        if(reflectionRemaining == 0)
        {
            origin = test[0];
            target = test[1];
            dist = Vector3.Distance(origin, target);
            lineRenderer.positionCount = test.Count;
            launchingDraw = true;
            return;
        }
        Vector3 startingPosition = position;

        Ray ray = new Ray(position, direction);
        RaycastHit hit;
        if(Physics.Raycast(ray ,out hit, maxStepDistance))
        {
            if(hit.transform.tag == "Wall")
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
                test.Add(position);
                origin = test[0];
                target = test[1];
                dist = Vector3.Distance(origin, target);
                lineRenderer.positionCount = test.Count;
                launchingDraw = true;
                return;
            }
            else
            {
                direction = Vector3.Reflect(direction, hit.normal);
                position = hit.point;
                test.Add(position);
            }
        }
        else
        {
            position += direction * maxStepDistance;
        }
        DrawPredictedReflectionsPattern(position, direction, reflectionRemaining - 1);
    }

    private void GetNextWayPoint()
    {
        counter = 0;
        wayPointIndex++;
        origin = test[wayPointIndex];
        target = test[wayPointIndex + 1];
        dist = Vector3.Distance(origin, target);
        nextPoint = Vector3.Distance(origin, target);
        for(int i=wayPointIndex; i< test.Count; i++)
        {
            lineRenderer.SetPosition(i, origin);
        }
    }
    void endLaser()
    {
        lineRenderer.enabled = false;
        launchingDraw = false;
        enabled = false;
        Destroy(this.gameObject);
    }
}
