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
    [SerializeField] private TextMeshProUGUI txtSupplyDistrubuted;

    [SyncVar(hook = nameof(UpdateManPowerText))] public int manPower;
    [SerializeField] private int maxManPower;

    [SyncVar(hook = nameof(UpdateSuppliesText))] public int supplyCount;
    [SerializeField] private int maxSupplyCount;

    [HideInInspector][SyncVar(hook = nameof(UpdateSupplyDistributed))] public int supplyDistributed;

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

    private void UpdateSupplyDistributed(int oldInt, int newInt)
    {
        txtSupplyDistrubuted.text = $"{newInt}";
    }

    public void IncreaseSupplyCount()
    {
        CmdIncreaseSupplyCount();
    }

    private void InternalIncreaseSupplyCount()
    {
        supplyCount += 1;
        supplyCount = Mathf.Clamp(supplyCount, 0, maxSupplyCount);
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
        manPower = Mathf.Clamp(manPower, 0, maxManPower);
    }
}
