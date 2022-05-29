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

    [SyncVar(hook = nameof(UpdateManPowerText))] public int manPower;
    [SerializeField] private int maxManPower;

    [SyncVar(hook = nameof(UpdateSuppliesText))] public int supplyCount;
    [SerializeField] private int maxSupplyCount;

    [SerializeField] private NetworkManager net;
    [SerializeField] private TextMeshProUGUI txtPlayer;

    private void Awake()
    {
        instance = this;
    }

    [Server]
    private void Start()
    {
        InvokeRepeating("DecayManPower", 0, 3);
    }

    private void Update()
    {
        if(net.numPlayers > 0)
        {
            txtPlayer.transform.parent.gameObject.SetActive(true);
            txtPlayer.text = net.numPlayers.ToString();
        }
        else
        {
            txtPlayer.transform.parent.gameObject.SetActive(false);
        }
    }

    private void UpdateManPowerText(int oldInt, int newInt)
    {
        txtManPower.text = $"{newInt} / {maxManPower}";
    }

    private void UpdateSuppliesText(int oldInt, int newInt)
    {
        txtSupplyCount.text = $"{newInt} / {maxSupplyCount}";
    }

    public void IncreaseSupplyCount()
    {
        CmdIncreaseSupplyCount();
    }

    private void InternalIncreaseSupplyCount()
    {
        supplyCount += 1;
        Mathf.Clamp(supplyCount, 0, maxSupplyCount);
    }

    [Command(requiresAuthority = false)]
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

    [Command(requiresAuthority = false)]
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
