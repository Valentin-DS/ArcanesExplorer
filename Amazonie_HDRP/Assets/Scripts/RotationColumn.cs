using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationColumn : MonoBehaviour
{
    public static bool rotationStart = false;
    public static Vector3 targetAngle;
    private static Vector3 currentAngle;
    public static Transform m_transform;
    public float speed = 0.02f;
    private static bool call_function = true;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        currentAngle = transform.eulerAngles;
        targetAngle = new Vector3(currentAngle.x, (currentAngle.y + 90), currentAngle.z);
    }

    // Update is called once per frame
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
