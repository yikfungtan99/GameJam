using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : NetworkBehaviour
{
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Boat Start");
        dir = new Vector2(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += dir * Time.deltaTime;
    }
}
