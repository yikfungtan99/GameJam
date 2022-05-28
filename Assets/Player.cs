using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : NetworkBehaviour
{
    private Camera cam;

    Vector3 mousePos;

    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private float clickRadius;

    [SerializeField] private float manPowerCDDuration;

    [SerializeField] private GameObject throwPrefab;
    [SerializeField] private float throwSpeed;
    [SerializeField] private float throwDuration;

    private float manPowerCDTime;

    private Boat boat;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        manPowerCDTime = 0;

        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
    }

    // Update is called once per frame
    void Update()
    {
        Input();

        if(manPowerCDTime > 0)
        {
            manPowerCDTime -= Time.deltaTime;
        }
        else
        {
            manPowerCDTime = 0;
        }
    }

    private void Input()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, clickLayer))
            {
                mousePos = hit.point;
            }
            
            InternalClick(mousePos);
        }
    }

    private void InternalClick(Vector3 mousePos)
    {
        if(manPowerCDDuration <= 0)
        {
            CmdClick(mousePos);
        }
    }

    [Command]
    private void CmdClick(Vector3 vector)
    {
        Vector3 targetVector = new Vector3(vector.x, 0, vector.z);
        
        GameObject go = Instantiate(throwPrefab, boat.transform.position, Quaternion.identity);
        NetworkServer.Spawn(go);
        go.transform.DOJump(targetVector, throwSpeed, 1, throwDuration);
    }

    
    [ClientRpc]
    private void RpcClick(Vector3 vector)
    {
        Debug.Log($"RPC: {vector}");
    }
}
