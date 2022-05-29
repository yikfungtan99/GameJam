using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Player : NetworkBehaviour
{
    private Camera cam;

    Vector3 mousePos;

    [SerializeField] private LayerMask clickLayer;
    [SerializeField] private float clickRadius;

    [SerializeField] private float throwCdDuration;

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
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (manPowerCDTime <= 0)
            {
                CmdClick(mousePos);
                manPowerCDTime = throwCdDuration;
            }
        }
    }

    [Command]
    private void CmdClick(Vector3 vector)
    {
        ThrowSupplies(vector);
    }

    private void ThrowSupplies(Vector3 vector)
    {
        ResourcesManager resource = ResourcesManager.Instance;
        if (resource.manPower <= 0 || resource.supplyCount <= 0) return;

        Vector3 targetVector = new Vector3(vector.x, 0, vector.z);

        GameObject go = Instantiate(throwPrefab, boat.transform.position, Quaternion.identity);
        NetworkServer.Spawn(go);
        go.transform.DOJump(targetVector, throwSpeed, 1, throwDuration);

        resource.supplyCount -= 1;
    }
}
