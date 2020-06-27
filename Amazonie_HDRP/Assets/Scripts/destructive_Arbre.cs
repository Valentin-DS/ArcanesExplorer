using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructive_Arbre : MonoBehaviour
{
    public int point_Vie_Arbre = 3;

    public GameObject objet_Buche;
    public GameObject arbre;

    private Animator animation_Arbre;
    private GameObject this_object;

    public List<GameObject> liste_Objet_Destructible = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        animation_Arbre = GetComponent<Animator>();
        point_Vie_Arbre = 3;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Hache" && other.GetComponent<Animation>().isPlaying && point_Vie_Arbre >0)
        {
            point_Vie_Arbre--;
            Debug.Log(point_Vie_Arbre);
            verif_Arbre();
        }
    }
    
    private void verif_Arbre()
    {
        if(point_Vie_Arbre <= 0)
        {
            animation_Arbre.SetTrigger("Decoupe");
            creation_Buche();
            StartCoroutine(launch_Respawn_Time());
        }
    }

    private void creation_Buche()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 positionBois = new Vector3(Random.Range(this.transform.position.x - 5, this.transform.position.x + 5), 0, Random.Range(this.transform.position.z - 5, this.transform.position.z + 5));
            GameObject nouvelObjet = Instantiate(objet_Buche, positionBois, objet_Buche.transform.rotation);
            nouvelObjet.name = "Bois";
        }
    }
    private void invisible_Partie()
    {
        for(int i = 0; i < liste_Objet_Destructible.Count; i++)
        {
            if (liste_Objet_Destructible[i].activeInHierarchy)
            {
                liste_Objet_Destructible[i].SetActive(false);
            }
            else
            {
                liste_Objet_Destructible[i].SetActive(true);
            }
            
        }
    }
    IEnumerator launch_Respawn_Time()
    {
        yield return new WaitForSeconds(5f);
        animation_Arbre.Play("Animation_Idle", 0, 0);
        invisible_Partie();
        yield return new WaitForSeconds(10f);
        invisible_Partie();
        GameObject new_Arbre = Instantiate(arbre, this.transform.position, this.transform.rotation);
        new_Arbre.name = "Amazonie_Arbre";
        
        Destroy(this.gameObject);
    }
}
