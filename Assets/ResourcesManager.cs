using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesManager : NetworkBehaviour
{
    private static ResourcesManager instance;
    public static ResourcesManager Instance { get { return instance; } }

    [SerializeField] private TextMeshProUGUI txtManPower;
    [SerializeField] private TextMeshProUGUI txtSupplyCount;

    [SyncVar(hook = nameof(UpdateText))] public int manPower;
    [SerializeField] private int maxManPower;

    [SyncVar(hook = nameof(UpdateText))] public int supplyCount;
    [SerializeField] private int maxSupplyCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating("DecayManPower", 0, 10);
        InvokeRepeating("DecaySupplyCount", 0, 10);
    }

    private void UpdateText()
    {
        txtManPower.text = $"{manPower} / {maxManPower}";
        txtSupplyCount.text = $"{supplyCount} / {maxSupplyCount}";
    }

    public void IncreaseSupplyCount()
    {
        CmdIncreaseSupplyCount();
    }

    private void InternalIncreaseSupplyCount()
    {
        manPower += 1;
        Mathf.Clamp(manPower, 0, maxManPower);
    }

    [Command]
    private void CmdIncreaseSupplyCount()
    {
        InternalIncreaseSupplyCount();
    }

    private void DecaySupplyCount()
    {
        if (supplyCount > 0)
        {
            supplyCount -= 1;
        }
    }

    private void DecayManPower()
    {
        if (manPower > 0)
        {
            manPower -= 1;
        }
    }

    public void IncreaseManPower()
    {
        CmdIncreaseManPower();
    }

    [Command]
    private void CmdIncreaseManPower()
    {
        InternalIncreaseManPower();
    }

    private void InternalIncreaseManPower()
    {
        manPower += 1;
        Mathf.Clamp(manPower, 0, maxManPower);
    }
}
