using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : NetworkBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed;

    private float currentProgress = 0;

    private void Update()
    {
        MovePayload(pointA.position, pointB.position, moveSpeed);
    }

    private void MovePayload(Vector2 ptA, Vector2 ptB, float spd)
    {
        float prog = currentProgress += spd * Time.deltaTime;
        transform.position = Vector2.Lerp(ptA, ptB, prog);
    }
}
