using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enigme1 : MonoBehaviour
{

    public List<int> listeCombinaisonEnigme = new List<int>();
    public List<GameObject> listeDalle = new List<GameObject>();

    private int indexActuel = 0;

    private int numPrec;
    public bool reussi = false;
    // Start is called before the first frame update
    void Start()
    {
        listeCombinaisonEnigme.Add(4);
        listeCombinaisonEnigme.Add(1);
        listeCombinaisonEnigme.Add(8);
        listeCombinaisonEnigme.Add(5);
    }

    public void verifCombinaison(int num)
    {
        if(numPrec != num)
        {
            if (num == listeCombinaisonEnigme[indexActuel])
            {
                indexActuel++;
                verifCombiEnd();
                reussi = true;
                Debug.Log("Réussi !");
                this.enabled = false;
                numPrec = num;
            }
            else
            {
                indexActuel = 0;
                Debug.Log("Raté !");
                numPrec = 0;
            }
        }
    }

    public void verifCombiEnd()
    {
        if(indexActuel == 4)
        {
            for (int i = 0; i < listeDalle.Count; i++)
            {
                Debug.Log("oki");
                listeDalle[i].GetComponent<Collider>().enabled = false;
            }
            Debug.Log("Vous avez réussi l'énigme !");
        }
    }
}
