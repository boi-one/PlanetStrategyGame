using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class SelectPlanet : MonoBehaviour
{
    public static Planet selectedPlanet = null; 
    public static Planet targetPlanet = null;

    private void OnMouseOver() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            //selectedPlanet = null; 
            foreach (Planet planet in ManagePlanets.system)
            {
                if (gameObject == planet.body && planet.capturedState != State.enemyCaptured)
                {
                    selectedPlanet = planet;
                    selectedPlanet.body = gameObject;
                    break;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //targetPlanet = null;
            foreach (Planet planet in ManagePlanets.system)
            {
                if (gameObject == planet.body && selectedPlanet != null)
                {
                    targetPlanet = planet;
                    targetPlanet.body = gameObject;
                    break;
                }
            }

            if (selectedPlanet.capturedState != State.enemyCaptured && selectedPlanet.shipsInOrbit.Count > 1 && selectedPlanet != null && targetPlanet != null && selectedPlanet != targetPlanet)
            {
                Ship ship = selectedPlanet.shipsInOrbit[0]; //??? 0 word niet null
                ship.body.transform.up = (ship.destination - ship.body.transform.position);
                ship.target = targetPlanet;
                selectedPlanet.shipsInOrbit.Remove(ship);
                ship.target.shipsInOrbit.Add(ship);
                ship.destination = ship.body.transform.position;
                ship.currentState = ShipStates.travel;
            }
        }

        
        //for debug purposes
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middel Click");
            
            Planet planet = null;
            GameObject planetGO = null;
            foreach (Planet p in ManagePlanets.system)
            {
                if (gameObject == p.body)
                {
                    planet = p;
                    planetGO = gameObject;
                    break;
                }
            }

            GameObject shipPrefab = GameObject.Find("Manager").GetComponent<ManageShips>().shipPrefab;
            Ship s = new Ship(Instantiate(shipPrefab, planet.position, Quaternion.identity),
                planet.body.transform.position, planet.position, planet, 2f, true);
            s.body.GetComponent<SpriteRenderer>().color = Color.blue;
            planet.shipsInOrbit.Add(s);
        }
    }
}
