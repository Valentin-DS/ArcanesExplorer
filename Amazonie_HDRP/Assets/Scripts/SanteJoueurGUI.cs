using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SanteJoueurGUI : MonoBehaviour
{
    [SerializeField] Image JaugeFaim;
    [SerializeField] Image JaugeSoif;
    [SerializeField] Image JaugeSommeil;
    public float CoefficientFaim { get; private set; }
    public float CoefficientSoif { get; private set; }
    public float CoefficientSommeil { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CoefficientFaim = 0.00008f;
        CoefficientSoif = 0.00004f;
        CoefficientSommeil = 0.00002f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Keypad1))
        {
            SanteJoueur.Instance.Faim += 0.25f;
        }

        if(Input.GetKey(KeyCode.Keypad2))
        {
            SanteJoueur.Instance.Soif += 0.25f;
        }

        if(Input.GetKey(KeyCode.Keypad3))
        {
            SanteJoueur.Instance.Sommeil += 1;
        }

        if(SanteJoueur.Instance.Faim >= 0)
        {
            SanteJoueur.Instance.Faim -= CoefficientFaim;
        }

        if(SanteJoueur.Instance.Soif >= 0)
        {
            SanteJoueur.Instance.Soif -= CoefficientSoif;
        }

        if(SanteJoueur.Instance.Sommeil >= 0)
        {
            SanteJoueur.Instance.Sommeil -= CoefficientSommeil;
        }

        JaugeFaim.fillAmount = SanteJoueur.Instance.Faim;
        JaugeSoif.fillAmount = SanteJoueur.Instance.Soif;
        JaugeSommeil.fillAmount = SanteJoueur.Instance.Sommeil;
    }
}
