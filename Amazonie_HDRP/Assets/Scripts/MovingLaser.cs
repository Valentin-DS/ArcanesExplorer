using UnityEngine;

/**
 * @warning Cette classe semble être un doublon de Laser : L'utiliser ou la supprimer
 */
public class MovingLaser : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private int wayPointIndex = 1;

    private Vector3 origin;
    private Vector3 target;
    private float dist;
    public float lineDrawSpeed = 60f;
    private float counter;
    private float nextPoint;
    // Start is called before the first frame update
    void Start()
    {
        origin = DrawLaser.listePosition[1];
        target = DrawLaser.listePosition[2];

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(1, origin);
        dist = Vector3.Distance(origin, target);
    }

    // Update is called once per frame
    void Update()
    {
        counter += 0.1f / lineDrawSpeed * Time.deltaTime;

        float x = Mathf.Lerp(0, dist, counter);

        Vector3 pointA = origin;
        Vector3 pointB = target;

        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        lineRenderer.SetPosition(wayPointIndex + 1, pointAlongLine);
        nextPoint = Vector3.Distance(pointB, pointAlongLine);
        if (nextPoint < 0.2)
        {

            if(wayPointIndex < 1)
            {
                getNextWayPoint();
            }
            else
            {
                enabled = false;
            }
        }
    }
    private void getNextWayPoint()
    {
        counter = 0;
        wayPointIndex++;
        origin = DrawLaser.listePosition[wayPointIndex];
        target = DrawLaser.listePosition[wayPointIndex + 1];
        nextPoint = Vector3.Distance(origin, target);
        lineRenderer.SetPosition(wayPointIndex, origin);
        
    }
}
