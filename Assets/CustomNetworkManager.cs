using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        GameObject go = Instantiate(spawnPrefabs[0]);
        NetworkServer.Spawn(go);
    }
}
