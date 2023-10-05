using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Planet planet in ManagePlanets.system)
        {
            foreach (Ship ship in planet.shipsInOrbit)
            {
                if (planet.capturedState == State.playerCaptured)
                {
                    
                }

                if (planet.capturedState == State.enemyCaptured)
                {
                    
                }
            }
        }
    }
}
