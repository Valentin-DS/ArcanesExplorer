using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart_Enigme_3 : MonoBehaviour
{
    public List<GameObject> liste_Dalles = new List<GameObject>();
    public GameObject restart_Pos;
    public GameObject player;
    public GameObject player_Prefab;
    private AudioSource BruitagePas;

    // Start is called before the first frame update
    private void Start()
    {
        BruitagePas = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {        
        reload_Dalle(other);     
    }

    private void reload_Dalle(Collider other)
    {
        for(int i=0; i<liste_Dalles.Count; i++)
        {
            liste_Dalles[i].SetActive(true);
        }

        Destroy(other.gameObject);
        GameObject nouvelleInstance = Instantiate(player_Prefab, restart_Pos.transform.position, restart_Pos.transform.rotation, null);
        nouvelleInstance.AddComponent<AudioSource>();
        nouvelleInstance.GetComponent<AudioSource>().clip = BruitagePas.clip;
        nouvelleInstance.AddComponent<AudioSource>().volume = BruitagePas.volume;
    }

}
