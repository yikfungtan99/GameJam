using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoConnect : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private string serverAddress;

    // Start is called before the first frame update
    void Start()
    {
        networkManager.networkAddress = serverAddress;
        networkManager.StartClient();
    }
}
