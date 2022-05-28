using Mirror;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : NetworkBehaviour
{
    [SerializeField] private PathFollower pathFollower;

    [SerializeField] private float speedIncrement = 1;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        InvokeRepeating("DecayManPower", 0, 10);
    }

    public void SetMoveSpeed(float spd)
    {
        pathFollower.speed = spd;
    }
}
