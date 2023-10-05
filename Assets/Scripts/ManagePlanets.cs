using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ManagePlanets : MonoBehaviour
{
    public int planetAmount;
    public GameObject planetPrefab;
    public static List<Planet> system = new List<Planet>();
    private int teams;

    private Color Interpolate(RGB a, RGB b)
    {
        float t = 0;
        return a + t * (b - a);
    }
    struct RGB
    {
        public float r, g, b;

        public RGB(float _r, float _g, float _b)
        {
            r = _r;
            g = _g;
            b = _b;
        }
    }; 
    private void TakeOverPlanetVisual(Planet planet, float colorChangingSpeed)
    {
        RGB beginColor;
        RGB endColor;
        Color planetColor;
        
        if(planet?.shipsInOrbit.Count > 0)
        {
            float interpolationSpeed = 0.1f;
            if (planet.amountBlue.Count - planet.amountRed.Count > 0 && planet.capturedColor.b < 0.5f)
            {
                planetColor = planet.body.GetComponent<SpriteRenderer>().color;
                beginColor = new RGB(planetColor.r, planetColor.g, planetColor.b);
                endColor = new RGB(1.0f, 0.0f, 0.0f);
                planetColor += Interpolate(beginColor, endColor);
                    
            }
            else if (planet.amountBlue.Count - planet.amountRed.Count < 0 && planet.capturedColor.r < 0.5f)
            {
                
            }
            Debug.Log(planet.capturedState + ": 1 itteration " + planet.capturedColor + " color value: " + planetColor);
        }
    }
    private void GenerateSystem(Planet newestPlanet, float distancePlanet)
    {
        for (int i = 0; i < planetAmount; i++)
        {
            system.Add(newestPlanet = new Planet(Instantiate(planetPrefab),new Vector3(Random.Range(-6f, 6f), Random.Range(-2f, 2f)), Random.Range(1f, 5f)));
            system[i].body.transform.position = system[i].position;
            system[i].body.transform.localScale = new Vector3(system[i].scale, system[i].scale);
        }
        foreach (Planet planet in system) //TODO: make it so planets don't overlap
        {
            if ((planet.body.transform.position - newestPlanet.body.transform.position).magnitude < distancePlanet)
                distancePlanet = (planet.body.transform.position - newestPlanet.body.transform.position).magnitude;
            float minimumPlanetDistance = (newestPlanet.scale * 0.5f) + (planet.scale * 0.5f);
            if (system.Count > 1 || distancePlanet < ((newestPlanet.scale * 0.5) + (planet.scale * 0.5f)))
            { 
                //Debug.Log("too close!");
                newestPlanet.position = new Vector3(Random.Range(-6f, 6f), Random.Range(-2f, 2f));
                newestPlanet.body.transform.position = newestPlanet.position;
            }
        }
    }
    private void AssignTeamsToPlanet()
    {
        foreach (Planet planet in system)
        {
            if (teams == 0)
            {
                planet.capturedState = State.playerCaptured;
                teams++;
            }
            else if (teams == 1)
            {
                planet.capturedState = State.enemyCaptured;
                teams++;
            }
            else
                planet.capturedState = State.uncaptured;
        }
    }

    private void Awake()
    {
        Planet newestPlanet = null;
        float distancePlanet = 99f;
        GenerateSystem(newestPlanet, distancePlanet);
        AssignTeamsToPlanet();
    }

    private void Update()
    {
        //spawning ships hoe meer ships er zijn hoe minders er spawnen om de zoveel seconde
        
        foreach(Planet planet in system)
        {
            //change color of planet
            //if more ships than enemyships over planet then slowly capture and kill of the enemy ships
            TakeOverPlanetVisual(planet, 10);
            
        }
    }
}


        
    
