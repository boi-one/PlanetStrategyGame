using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Planet
{
    public GameObject body;
    public Vector3 position;

    public Color capturedColor;
    public float scale;
    public bool captured;
    public State capturedState;
    public List<Ship> shipsInOrbit = new List<Ship>(), amountBlue = new List<Ship>(), amountRed = new List<Ship>();
    public Planet(GameObject body, Vector3 position, float scale)
    {
        this.body = body;
        this.position = position;
        this.scale = scale;
    }
}
public enum State
{
    playerCaptured,
    enemyCaptured,
    uncaptured
}
