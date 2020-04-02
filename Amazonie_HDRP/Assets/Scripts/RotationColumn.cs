using UnityEngine;

/**
 * @class RotationColumn
 * La classe RotationColumn permet d'exercer une rotation sur une colonne, dans la première salle du temple
 * @author Basile
 */
public class RotationColumn : MonoBehaviour
{
    /**
     * Booléen déterminant si la rotation est effectuée ou non
     */
    public static bool rotationStart = false;
    /**
     * Rotation cible
     */
    public static Vector3 targetAngle;
    /**
     * Rotation initiale
     */
    private static Vector3 currentAngle;
    /**
     * Composant transform de la colonne
     */
    public static Transform m_transform;
    /**
     * Vitesse de rotation de la colonne
     */
    public float speed = 0.02f;
    /**
     * Booléen déterminant si la fonction de rotation a été appellée
     */
    public static bool call_function = true;
    /**
     * Initialisation des paramètres
     */
    void Start()
    {
        m_transform = this.transform;
        currentAngle = transform.eulerAngles;
        targetAngle = new Vector3(currentAngle.x, (currentAngle.y + 90), currentAngle.z);
    }
    /**
     * Boucle principale de RotationColumn
     */
    void Update()
    {
        if (rotationStart)
        {
            currentAngle = new Vector3(currentAngle.x, Mathf.Lerp(currentAngle.y, targetAngle.y, speed), currentAngle.z);
            m_transform.eulerAngles = currentAngle;
            if(Mathf.Abs(m_transform.eulerAngles.y - targetAngle.y) < 0.5f)
            {
                m_transform.eulerAngles = targetAngle;
                call_function = true;
                rotationStart = false;  
            }
        }
        
    }
    /**
     * Méthode permettant d'exercer la rotation sur la colonne
     * @param Nom de la colonne
     */
    public static void startRotation(string name)
    {
        m_transform = GameObject.Find(name).GetComponent<Transform>();
        if (call_function)
        {
            currentAngle = m_transform.eulerAngles;
            targetAngle = new Vector3(currentAngle.x, (currentAngle.y + 90), currentAngle.z);
            rotationStart = true;
            call_function = false;
        }
        else
        {
            return;
        }
    }
}
