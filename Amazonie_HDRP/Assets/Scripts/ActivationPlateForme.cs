using System.Collections.Generic;
using UnityEngine;

/**
 * @class ActivationPlateForme
 * La classe ActivationPlateForme correspond à l'activation et le déplacement de la plateforme à la fin du labyrinthe (2e salle du temple)
 * @author Basile
 */
public class ActivationPlateForme : MonoBehaviour
{
    /**
     * Le temps ou le joueur arrive sur la plateforme
     */
    private float startTime;
    /**
     * Distance entre le point de départ et d'arrivée de la plateforme
     */
    private float journeyLength;
    /**
     * Liste des positions auxquelles le joueur doit arriver lors du déplacement de la plateforme
     */
    public List<Transform> endpoint = new List<Transform>();
    /**
     * Vitesse d'élévation de la plateforme
     */
    public float speed = 1f;
    /**
     * Booléen d'activation ou non du déplacement de la plateforme
     */
    private bool activatePlate = false;
    /**
     * Index de navigation dans la liste
     * @see List<Transform> endpoint
     */
    private int index = 0;
    /**
     * Distance entre la position courante du joueur et la prochaine position qu'il doit atteindre
     */
    private float distance;
    private AudioSource BruitagePlateforme;

    private void Start()
    {
        BruitagePlateforme = GetComponent<AudioSource>();
    }

    /**
     * Boucle principale d'ActivationPlateForme
     */
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

    /**
     * Chargement du prochain point à atteindre
     */
    private void loadNextPoint()
    {
        startTime = Time.time;
        index++;
        journeyLength = Vector3.Distance(this.transform.position, endpoint[index].position);
        activatePlate = true;
    }

    /**
     * Détecte la présence du joueur sur la plateforme
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerEnter(Collider other)
    {
        if (index == 0)
        {
            BruitagePlateforme.Play();
            startTime = Time.time;

            journeyLength = Vector3.Distance(this.transform.position, endpoint[index].position);
            activatePlate = true;
            other.transform.parent = this.transform;
        }
    }

    /**
     * Détecte la sortie du joueur de la plateforme
     * @param other : Collider qui est detecté à la collision
     */
    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }
}
