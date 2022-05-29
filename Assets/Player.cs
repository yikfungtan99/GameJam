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

    private PlayerActionManager playerAction;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        manPowerCDTime = 0;

        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<Boat>();
        playerAction = PlayerActionManager.Instance;
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
        if (!hasAuthority) return;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (playerAction.throwCooldownTime <= 0 && ResourcesManager.Instance.supplyCount > 0)
            {
                CmdClick(mousePos);
                PlayerActionManager.Instance.ThrowCooldown();
            }
        }
#endif
#if UNITY_ANDROID
        
        if (!TouchOnUI())
        {
            if (playerAction.throwCooldownTime <= 0 && ResourcesManager.Instance.supplyCount > 0)
            {
                CmdClick(mousePos);
                PlayerActionManager.Instance.ThrowCooldown();
            }
        }
#endif
    }

    private bool TouchOnUI()
    {
        bool onUI = false;
        foreach (Touch touch in UnityEngine.Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                onUI = true;
            }
        }

        return onUI;
    }

    [Command]
    private void CmdClick(Vector3 vector)
    {
        ThrowSupplies(vector);
    }

    private void ThrowSupplies(Vector3 vector)
    {
        ResourcesManager resource = ResourcesManager.Instance;
        if (resource.supplyCount <= 0) return;

        resource.supplyCount -= 1;

        Vector3 targetVector = new Vector3(vector.x, 0, vector.z);

        GameObject go = Instantiate(throwPrefab, boat.transform.position, Quaternion.identity);
        NetworkServer.Spawn(go);
        go.transform.DOJump(targetVector, throwSpeed, 1, throwDuration);

    }
}
