using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supplies : NetworkBehaviour
{
    [SerializeField] private float decayDuration;

    private void Start()
    {
        Invoke("DestroyThis", decayDuration);
    }

    private void DestroyThis()
    {
        NetworkServer.Destroy(gameObject);
    }
}
