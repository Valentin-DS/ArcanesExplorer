using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageSigne : MonoBehaviour
{

    public GameObject plane_Obj;
    public List<Material> liste_Mat = new List<Material>();

    public void affichage(int num)
    {
        plane_Obj.GetComponent<Renderer>().material = liste_Mat[num];
    }
}
