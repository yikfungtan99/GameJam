using Mirror;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : NetworkBehaviour
{
    [SerializeField] private PathFollower pathFollower;

    [SerializeField] private float speed;

    private float curSpeed = 0;

    private void Update()
    {
        SpeedControl();
    }

    private void SpeedControl()
    {
        if (ResourcesManager.Instance.manPower <= 0)
        {
            if(curSpeed > 0)
            {
                curSpeed = Mathf.Lerp(curSpeed, 0, Time.deltaTime);
            }
            
        }
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, speed, Time.deltaTime);
        }


        pathFollower.speed = curSpeed;
    }
}
