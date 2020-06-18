using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComportementArbre : MonoBehaviour
{
    bool animationJouee;
    int nombreCoups;
    [SerializeField] GameObject outil;
    [SerializeField] GameObject partieHauteArbre;
    [SerializeField] GameObject boisARamasser;
    Animation animationArbre;
    Animation animationHache;
    GameObject objetParent;
    Timer instanceTimer;
    bool partieHauteDisparue;
    Vector3 positionArbreOrigine;
    Quaternion orientationArbreOrigine;

    // Start is called before the first frame update
    void Start()
    {
        animationArbre = partieHauteArbre.GetComponent<Animation>();
        animationArbre.clip.legacy = true;
        animationJouee = false;
        nombreCoups = 0;
        objetParent = transform.parent.gameObject;
        if(outil.transform.GetChild(0).GetComponent<Animation>() != null)
            animationHache = outil.transform.GetChild(0).GetComponent<Animation>();
        instanceTimer = GetComponent<Timer>();
        partieHauteDisparue = false;
        positionArbreOrigine = partieHauteArbre.GetComponent<Transform>().position;
        orientationArbreOrigine = partieHauteArbre.GetComponent<Transform>().rotation;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Hache") && animationHache.isPlaying && !animationJouee)
        {
            //&& animationHache["AnimationHache"].time < 0.5f
            nombreCoups++;
            Debug.Log(nombreCoups);
            if (nombreCoups == 3)
            {
                animationArbre.Play();
                animationJouee = true;
                for (int i = 0; i < 3; i++)
                {
                    Vector3 positionBois = new Vector3(Random.Range(objetParent.transform.position.x - 5, objetParent.transform.position.x + 5), 0, Random.Range(objetParent.transform.position.z - 5, objetParent.transform.position.z + 5));
                    GameObject nouvelObjet = Instantiate(boisARamasser, objetParent.transform);
                    nouvelObjet.transform.position = positionBois;
                }

                nombreCoups = 0;
            }
        }
    }

    void Update()
    {
        if(animationJouee && !animationArbre.isPlaying && !partieHauteDisparue)
        {
            partieHauteArbre.SetActive(false);
            instanceTimer.enabled = true;
            partieHauteDisparue = true;
        }
        else if(partieHauteDisparue && instanceTimer.tempsCourant >= 10)
        {
            instanceTimer.Reinitialise();
            instanceTimer.enabled = false;
            partieHauteArbre.SetActive(true);
            partieHauteArbre.GetComponent<Transform>().position = positionArbreOrigine;
            partieHauteArbre.GetComponent<Transform>().rotation = orientationArbreOrigine;
            partieHauteDisparue = false;
            animationJouee = false;
        }
    }
}
