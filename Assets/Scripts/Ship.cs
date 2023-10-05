using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : MonoBehaviour
{
    public GameObject body;
    public Vector3 spawnPosition, destination;
    public bool blueTeam;
    public Planet target;
    public float speed;
    public ShipStates currentState;
    public Ship kill;
    public int hp = 5;

    public float cooldown = 1.5f;
    public float fireRate = 2.0f;
    public Ship(GameObject body, Vector3 spawnPosition, Vector3 destination, Planet target, float speed, bool blueTeam)
    {
        this.body = body;
        this.spawnPosition = spawnPosition;
        this.destination = destination;
        this.target = target;
        this.speed = speed;
        this.blueTeam = blueTeam;
    }
}
public enum ShipStates
{
    patrol,
    travel,
    attack
}
