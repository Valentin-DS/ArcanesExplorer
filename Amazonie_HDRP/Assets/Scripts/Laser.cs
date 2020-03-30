using System.Collections.Generic;
using UnityEngine;

/**
 * @class Laser
 * La classe Laser permet de gérer le déclenchement d'un laser (et le lancement de l'animation d'ouverture de la porte dans la première salle du temple notamment)
 * @author Basile
 * */
public class Laser : MonoBehaviour
{
    /**
     * Nombre de réflexions du laser
     */
    public int maxReflectionCount = 5;
    /**
     * Distance maximale du laser
     */
    public float maxStepDistance = 25;
    /**
     * Objet porte sur lequel l'animation va être déclenchée
     */
    public GameObject porte;
    /**
     * Booléen indiquant si le laser doit être déclenché ou non
     */
    private bool launchingDraw = false;
    /**
     * Coordonnées du laser
     * @todo Renommer la variable
     */
    public List<Vector3> test = new List<Vector3>();
    /**
     * Coordonnée de fin du laser, le point qu'il doit atteindre
     */
    public Vector3 finishLaser = new Vector3(-5.44f, 8.29f, 11.3f);
    /**
     * Coordonée d'origine du laser (colonne)
     */
    private Vector3 origin;
    /**
     * Coordonnée cible du laser (colonne)
     */
    private Vector3 target;
    /**
     * Rendu graphique du laser
     */
    private LineRenderer lineRenderer;
    /**
     * Distance entre deux colonnes reliées par le laser
     * @see Vector3 origin
     * @see Vector3 target
     */
    private float dist;
    /**
     * Compteur de progression de la distance parcourue par le laser entre deux colonnes
     */
    private float counter;
    /**
     * Vitesse du rendu graphique du laser
     */
    public float lineDrawSpeed = 6f;
    /**
     * Compteur du nombre de réflexions du laser
     * @see int maxReflectionCount
     */
    private int wayPointIndex = 0;
    /**
     * Distance séparant une réflexion d'une autre
     */
    private float nextPoint;

    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        porte = GameObject.Find("Temple_Porte");
        lineRenderer = GetComponent<LineRenderer>();
        test.Add(this.transform.position);
        lineRenderer.SetPosition(0, test[0]);
        DrawPredictedReflectionsPattern(this.transform.position + this.transform.forward * 0.75f, this.transform.forward, maxReflectionCount);
    }

    /**
     * Boucle principale de Laser
     */
    void Update()
    {
        Debug.Log(nextPoint);
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

    /**
     * Méthode permettant de tracer le laser d'une réflexion à une autre
     * @param position : Position initiale du laser
     * @param direction : Position finale du laser
     * @param reflectionRemaining : Nombre de réflexions restantes
     */
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

    /**
     * Méthode permettant de définir le prochain point à atteindre
     */
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

    /**
     * Méthode permettant de supprimer le laser
     */
    void endLaser()
    {
        lineRenderer.enabled = false;
        launchingDraw = false;
        enabled = false;
        Destroy(this.gameObject);
    }
}
