using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    public static HUD Instance;
    [SerializeField] AnimationClip animationAller;
    [SerializeField] AnimationClip animationRetour;
    Animation animationBras;
    public bool AnimationJouee;
    void Start()
    {
        animationBras = this.GetComponent<Animation>();
        animationAller.legacy = true;
        animationRetour.legacy = true;
        animationBras.AddClip(animationAller, Constantes.BRAS_ANIMATION_ALLER);
        animationBras.AddClip(animationRetour, Constantes.BRAS_ANIMATION_RETOUR);
        animationBras.clip = animationBras.GetClip(Constantes.BRAS_ANIMATION_ALLER);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!AnimationJouee) 
            {
                AnimationJouee = true;
                animationBras.clip = animationBras.GetClip(Constantes.BRAS_ANIMATION_ALLER);
            }
            else
            {
                AnimationJouee = false;
                animationBras.clip = animationBras.GetClip(Constantes.BRAS_ANIMATION_RETOUR);
            }

            animationBras.Play();
        }
    }
}
