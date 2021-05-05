using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public Light sun;
    public float secondsInDay = 120f;

    [Range(0, 1)] public float currentTimeOfDay = 0;

    private float timeMultiplier = 1f;
    private float sunInitialIntensity;

    private GameObject[] StreetLights;
    private GameObject[] CookieLights;
    private Material[] materials;

    public Material lightsOnMaterial;
    public Material lightsOffMaterial;

    private bool isLightsOn = false;

    void Start()
    {
        sunInitialIntensity = sun.intensity;

        StreetLights = GameObject.FindGameObjectsWithTag("StreetLights");
        CookieLights = GameObject.FindGameObjectsWithTag("CookieLights");

        materials = GetComponent<Renderer>().materials;

    }


    void Update()
    {
        UpdateSun();
        UpdateLights();

        currentTimeOfDay += (Time.deltaTime / secondsInDay) * timeMultiplier;
        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
        float intensityMultiplier = 1;
        isLightsOn = false;

        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
            isLightsOn = true;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;
    }

    void UpdateLights()
    {
        foreach (GameObject i in StreetLights)
        {
            i.SetActive(isLightsOn);
        }

        foreach (GameObject i in CookieLights)
        {
            i.SetActive(isLightsOn);
        }
        if (isLightsOn)
        {
            materials[2] = lightsOnMaterial;
        }
        else
        {
            materials[2] = lightsOffMaterial;
            
        }
        GetComponent<Renderer>().materials = materials;
    }

}
