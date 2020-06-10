using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    public bool foundation;
    public List<Collider> col = new List<Collider>();
    public Material green;
    public Material red;
    public bool IsBuildable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 18 && foundation)
        {
            col.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 18 && foundation)
        {
            col.Remove(other);
        }
    }

    public void changeColor()
    {
        if (col.Count == 0)
        {
            IsBuildable = true;
        }
        else
        {
            IsBuildable = false;
        }
      
        if (IsBuildable)
        {
            foreach(Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = green;
            }
        }
        else
        {
            foreach (Transform child in this.transform)
            {
                child.GetComponent<Renderer>().material = red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
    }
}

public enum objectsorts
{
    normal,
    foundation,
    floor
}
