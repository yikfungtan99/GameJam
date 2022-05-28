using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private Camera cam;

    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            CmdClick(mousePos);
        }
    }

    [Command]
    private void CmdClick(Vector3 vector)
    {
        Debug.Log($"CMD: {vector}");
        RpcClick(vector);
    }

    [ClientRpc]
    private void RpcClick(Vector3 vector)
    {
        Debug.Log($"RPC: {vector}");
    }
}
