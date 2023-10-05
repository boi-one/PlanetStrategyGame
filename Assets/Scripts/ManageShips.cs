using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ManageShips : MonoBehaviour
{
    public int startingAmount;
    public GameObject shipPrefab;

    void ShipStateMachine(Ship ship)
    {
        switch (ship.currentState)
                {
                    case ShipStates.patrol:
                        if ((ship.body.transform.position - ship.destination).magnitude < 0.1f)
                        {
                            //ooit maken dat er een random positie in een sphere is
                            ship.destination = new Vector3(Random.Range(ship.target.body.transform.position.x - ship.target.scale *0.5f, ship.target.body.transform.position.x + ship.target.scale * 0.5f), 
                                Random.Range(ship.target.body.transform.position.y - ship.target.scale * 0.5f, ship.target.body.transform.position.y + ship.target.scale * 0.5f));
                            ship.body.transform.up = (ship.destination - ship.body.transform.position);
                            

                            
                            //switch planet
                        }
                        break;
                    case ShipStates.travel:
                        ship.destination = ship.target.body.transform.position;
                        Debug.DrawLine(ship.body.transform.position, ship.destination, Color.magenta);
                        if ((ship.body.transform.position - ship.destination).magnitude < 0.1f)
                        {
                            ship.speed = ship.speed * 0.7f;
                            ship.currentState = ShipStates.attack;
                        }
                        break;
                    case ShipStates.attack:
                        //attacks the first enemy ship
                        List<Ship> enemyships = ship.target.shipsInOrbit.Where(s => !s.blueTeam).ToList();
                        ship.kill = enemyships[0];
                        ship.kill.body.GetComponent<SpriteRenderer>().color = Color.green;

                        ship.destination = ship.kill.transform.position;
                        
                        Debug.Log(ship.hp + " K " + ship.kill.hp);
                        
                        if (Time.time > ship.cooldown)
                        {
                            ship.kill.hp--;
                            Debug.Log("HIT");
                            ship.cooldown = Time.time + ship.fireRate;
                        }

                        if (ship.kill != null && ship.kill.hp < 1)
                        {
                            ship.speed = 2f;
                            ship.currentState = ShipStates.patrol;
                        }
                        break;
                }
    }
    
    private void Start()
    {
        foreach (Planet planet in ManagePlanets.system)
        {
            bool isblue = true;

            //assign teams
            if (planet.capturedState == State.playerCaptured)
                isblue = true;
            else
                isblue = false;
            
            //starting amount ships
            if(planet.capturedState == State.playerCaptured || planet.capturedState == State.enemyCaptured)
            {
                for (int i = 0; i < startingAmount; i++)
                {
                    planet.shipsInOrbit.Add(new Ship(Instantiate(shipPrefab, planet.position, Quaternion.identity), planet.body.transform.position, planet.position, planet, 2f, isblue));
                }
            }
            
            //color planet
            foreach (Ship s in planet.shipsInOrbit)
            {
                if (s.blueTeam)
                {
                    s.body.GetComponent<SpriteRenderer>().color = Color.blue;
                    planet.amountBlue.Add(s);
                }
                else
                {
                    s.body.GetComponent<SpriteRenderer>().color = Color.red;
                    planet.amountRed.Add(s);
                }
            }
            //Debug.Log(p.amountBlue.Count + " " + p.amountRed.Count);
        }
    }
    void Update()
    {
        foreach(Planet planet in ManagePlanets.system)
        {
            foreach (Ship ship in planet.shipsInOrbit.ToList())
            {
                if(ship.hp < 1 && planet.shipsInOrbit.Contains(ship))
                    planet.shipsInOrbit.Remove(ship);

                ship.body.transform.position += (ship.destination - ship.body.transform.position).normalized * (ship.speed * Time.deltaTime);
                
                ShipStateMachine(ship);
                
                //check on which team the majority of ships are, then slowly transition to that color if it isnt captured already (in ManagePlanet.cs)
            }
            //amount of ships from the dominant team orbiting the planet
            planet.body.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = planet.shipsInOrbit.Count.ToString();
        }
    }
}



