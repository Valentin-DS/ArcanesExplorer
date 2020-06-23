using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Houe : MonoBehaviour
{
    public GameObject cam;
    private RaycastHit hit;
    [SerializeField] private float size = 1;
    public GameObject terreCultivable;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 20))
        {
        
            if (Input.GetMouseButtonDown(0) && hit.transform.name == "Terrain")
            {
            
                //Jouer l'animation de la houe
                Vector3 positionTerre = getNearestSpot(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                Instantiate(terreCultivable, positionTerre, Quaternion.identity);
                
            }
            else if(Input.GetMouseButtonDown(0) && hit.transform.name.Contains("TerreCulti"))
            {
                if(hit.transform.GetComponent<TerreCultivable>().is_Recoltable == true)
                {
                    //Recuperer le legume et l'ajouter dans l'inventaire
                    //Jouer une animation 
                    //Remettre un objet Terre_Cultivable de base
                    Debug.Log("LA CULTURE A ETE RECUPERE");
                }
            }
        }
    }

    public Vector3 getNearestSpot(Vector3 mousePos)
    {
        int xCount = Mathf.RoundToInt(mousePos.x / size);
        int yCount = Mathf.RoundToInt(mousePos.y / size);
        int zCount = Mathf.RoundToInt(mousePos.z / size);

        Vector3 result = new Vector3((float)xCount * size, (float)yCount * size, (float)zCount * size);
        return result;
    }
}
