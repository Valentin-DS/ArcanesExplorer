using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;
using UnityEngine.Rendering;

public class Day_Night_Cycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;
    [SerializeField] private float secondsInFullDay = 120f;

    [SerializeField] private float currentTimeOfDay = 0;

    private float timeMultiplier = 1f;
    private float sunInitialIntensity;

    //Récupération du système de dégradé 
    private Volume test_Bis;
    private GradientSky test;
    //Variables des couleurs du ciel de nuit
    private Color color_Night_Top = new Color(0f,0f,0f);
    private Color color_Night_Middle = new Color(0.019f, 0.019f, 0.019f);
    private Color color_Night_Bottom = new Color(0.019f, 0.019f, 0.019f);

    //Variables des couleurs du ciel de jour
    private Color color_Day_Top = new Color(0f, 0.164f, 0.98f);
    private Color color_Day_Middle = new Color(0.305f, 0.517f, 1f);
    private Color color_Day_Bottom = new Color(0.349f, 0.603f, 1f);

    //Variables des couleurs d'un orage
    private Color color_Lightning_Top = new Color(0.043f, 0.047f, 0.054f);
    private Color color_Lightning_Middle = new Color(0.039f, 0.031f, 0.031f);
    private Color color_Lightning_Bottom = new Color(0f, 0f, 0f);

    private Color color_Actual_Top;
    private Color color_Actual_Middle;
    private Color color_Actual_Bottom;

    public int timeMultiple;
    public GameObject sun_Go;

    //Récupération des différents éléments climatiques (particle_system)
    public GameObject particle_Clouds_Day;
    public GameObject particle_Clouds_Night;
    public GameObject particle_Clouds_Rain;
    public GameObject particle_Rain;
    public GameObject particle_Lightning;
    public GameObject particle_Myst;

    //Variables de gestion des éléments météo
    [SerializeField]
    private bool rain_Presence;
    [SerializeField]
    private bool lightning_Presence;
    [SerializeField]
    private bool myst_Presence;

    [SerializeField]
    private float rain_start_Time;
    [SerializeField]
    private float lightning_start_Time;
    [SerializeField]
    private float myst_start_Time;

    [SerializeField]
    private float rain_time;
    [SerializeField]
    private float lightning_time;
    [SerializeField]
    private float myst_time;

    private bool stop_degrade = false;

    private bool prec_Light = false;

    public Cultivable_Manager cultivable_Manage;
    // Start is called before the first frame update
    void Start()
    {
        sunInitialIntensity = sun.intensity;
        test_Bis = GetComponent<Volume>();
        test_Bis.profile.TryGet(out test);
    }

    // Update is called once per frame
    void Update()
    {
        //sun_Go.transform.RotateAround(new Vector3(Time.time, -30, 0), new Vector3(90,0,90), timeMultiple * Time.deltaTime);

        UpdateSun();

        //Avancé de la durée du jour
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        //Quand on a terminé une journée, on remet à 0
        if(currentTimeOfDay >= 1)
        {
            
            cultivable_Manage.terre_Jour_Suivant();
            currentTimeOfDay = 0;
            stop_degrade = false;
            particle_Rain.SetActive(false);
            particle_Lightning.SetActive(false);
            particle_Clouds_Rain.SetActive(false);
            particle_Myst.SetActive(false);
            Future_Weather();
        }
    }
    void UpdateSun()
    {
        //Rotation de la directional light agissant comme un soleil
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
        moon.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 270, 170, 0);

        //Variable gérant l'intensité du soleil
        float intensityMultiplier = 1;

        //Periode de la nuit
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            if (sun.gameObject.activeInHierarchy)
            {
                sun.gameObject.SetActive(false);
                moon.gameObject.SetActive(true);
            }
            intensityMultiplier = 0;
        }

        //Periode du jour
        else
        {
            if (moon.gameObject.activeInHierarchy)
            {
                moon.gameObject.SetActive(false);
                sun.gameObject.SetActive(true);
            }
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        //Periode du jour gérant le dégradé du ciel
        if(currentTimeOfDay >= 0.125f && currentTimeOfDay <= 0.375f)
        {
            if(particle_Clouds_Day.activeInHierarchy == false && particle_Clouds_Rain.activeInHierarchy == false)
            {
                particle_Clouds_Day.SetActive(true);
                particle_Clouds_Night.SetActive(false);
            }
            if(!stop_degrade)
            {
                if(prec_Light)
                {
                    float fracJourney = 1 - (0.375f - currentTimeOfDay) / 0.25f;
                    test.top.value = Color.Lerp(color_Lightning_Top, color_Day_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Lightning_Middle, color_Day_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Lightning_Bottom, color_Day_Bottom, fracJourney);
                }
                else
                {
                    float fracJourney = 1 - (0.375f - currentTimeOfDay) / 0.25f;
                    test.top.value = Color.Lerp(color_Night_Top, color_Day_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Night_Middle, color_Day_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Night_Bottom, color_Day_Bottom, fracJourney);
                }
            }
        }

        //Periode de la nuit gérant le dégradé du ciel
        else if(currentTimeOfDay >= 0.625f && currentTimeOfDay <= 0.875f)
        {
            if (particle_Clouds_Night.activeInHierarchy == false && particle_Clouds_Rain.activeInHierarchy == false)
            {
                particle_Clouds_Night.SetActive(true);
                particle_Clouds_Day.SetActive(false);
            }
            if (!stop_degrade)
            {
                float fracJourney = 1 - (0.875f - currentTimeOfDay) / 0.25f;
                // Debug.Log("Degradé de la couleur de la nuit : " + fracJourney);
                test.top.value = Color.Lerp(color_Day_Top, color_Night_Top, fracJourney);
                test.middle.value = Color.Lerp(color_Day_Middle, color_Night_Middle, fracJourney);
                test.bottom.value = Color.Lerp(color_Day_Bottom, color_Night_Bottom, fracJourney);
            }               
        }

        //Gestion de la mise en place du système de pluie
        if(rain_Presence && currentTimeOfDay >= rain_start_Time && !particle_Rain.activeInHierarchy)
        {
            particle_Rain.SetActive(true);
            particle_Clouds_Rain.SetActive(true);

            color_Actual_Top = test.top.value;
            color_Actual_Middle = test.middle.value;
            color_Actual_Bottom = test.bottom.value;

            stop_degrade = true;

            if (particle_Clouds_Day.activeInHierarchy)
            {
                particle_Clouds_Day.SetActive(false);
            }
            else
            {
                particle_Clouds_Night.SetActive(false);
            }
        }

        //Gestion de l'arrêt de la pluie
        if (particle_Rain.activeInHierarchy && currentTimeOfDay >= (rain_start_Time + rain_time))
        {
            particle_Rain.SetActive(false);
            particle_Clouds_Rain.SetActive(false);
        }

        //Gestion de la mise en place de l'orage
        if (lightning_Presence && currentTimeOfDay >= lightning_start_Time && !particle_Lightning.activeInHierarchy)
        {
            particle_Lightning.SetActive(true);
            particle_Clouds_Rain.SetActive(true);

            color_Actual_Top = test.top.value;
            color_Actual_Middle = test.middle.value;
            color_Actual_Bottom = test.bottom.value;

            stop_degrade = true;

            if (particle_Clouds_Day.activeInHierarchy)
            {
                particle_Clouds_Day.SetActive(false);
            }
            else
            {
                particle_Clouds_Night.SetActive(false);
            }
        }

        //Gestion de l'arrêt de l'orage
        if(particle_Lightning.activeInHierarchy && currentTimeOfDay >= (lightning_start_Time + lightning_time))
        {
            particle_Lightning.SetActive(false);
            particle_Clouds_Rain.SetActive(false);
        }

        //Gestion de la mise en place du brouillard
        if (myst_Presence && currentTimeOfDay >= myst_start_Time && !particle_Myst.activeInHierarchy)
        {
            particle_Myst.SetActive(true);
        }

        //Gestion de l'arrêt du brouillard
        if (particle_Myst.activeInHierarchy && currentTimeOfDay >= (myst_start_Time + myst_time))
        {
            particle_Myst.SetActive(false);
        }
        if (stop_degrade)
        {
            if(particle_Rain.activeInHierarchy)
            {
                prec_Light = true;
                float fracJourney = 1 / ((rain_time)/3) * currentTimeOfDay + (1 - (1/ ((rain_time/3))) * (rain_start_Time + (rain_time/3)));
                if (fracJourney < 1)
                {
                    test.top.value = Color.Lerp(color_Actual_Top, color_Lightning_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Actual_Middle, color_Lightning_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Actual_Bottom, color_Lightning_Bottom, fracJourney);
                }                
            }
            else if (particle_Lightning.activeInHierarchy)
            {
                prec_Light = true;
                float fracJourney = 1 / ((lightning_time) / 3) * currentTimeOfDay + (1 - (1 / ((lightning_time / 3))) * (lightning_start_Time + (lightning_time / 3)));
                if (fracJourney < 1)
                {
                    test.top.value = Color.Lerp(color_Actual_Top, color_Lightning_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Actual_Middle, color_Lightning_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Actual_Bottom, color_Lightning_Bottom, fracJourney);
                }
            }
            else
            {
                if(currentTimeOfDay >= 0.125f && currentTimeOfDay <= 0.375f)
                {
                    prec_Light = false;
                    float fracJourney = 1 - (0.375f - currentTimeOfDay) / 0.25f;
                    test.top.value = Color.Lerp(color_Lightning_Top, color_Day_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Lightning_Middle, color_Day_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Lightning_Bottom, color_Day_Bottom, fracJourney);
                }
                else if(currentTimeOfDay >= 0.625f && currentTimeOfDay <= 0.875f)
                {
                    prec_Light = false;
                    float fracJourney = 1 - (0.875f - currentTimeOfDay) / 0.25f;
                    test.top.value = Color.Lerp(color_Lightning_Top, color_Night_Top, fracJourney);
                    test.middle.value = Color.Lerp(color_Lightning_Middle, color_Night_Middle, fracJourney);
                    test.bottom.value = Color.Lerp(color_Lightning_Bottom, color_Night_Bottom, fracJourney);
                }
            }
        }
        //Gestion de l'intensité du soleil
        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    private void Future_Weather()
    {
        //Remise à zéro des éléments météo
        rain_Presence = false;
        lightning_Presence = false;
        myst_Presence = false;

        //Génération d'un nombre aléatoire pour chaqué éléments météo
        float rain_Presence_Rand = (int) Random.Range(0f,5f);
        float lightning_Presence_Rand = (int) Random.Range(0f, 7f);
        float myst_Presence_Rand = (int) Random.Range(0f, 7f);

        //Déduction de la présence d'un élément météo
        if(rain_Presence_Rand == 4)
        {
            rain_Presence = true;
            rain_start_Time = Random.Range(0f, 70f) / 100;
            rain_time = Random.Range(20f, 70f) / 100;
        }
        if(lightning_Presence_Rand == 6)
        {
            lightning_Presence = true;
            lightning_start_Time = Random.Range(0f, 60f) / 100;
            lightning_time = Random.Range(15f, 40f) / 100;
        }
        if(myst_Presence_Rand == 6)
        {
            myst_Presence = true;
            myst_start_Time = Random.Range(0f, 60f) / 100;
            myst_time = Random.Range(20f, 50f) / 100;
        }
    }
}
