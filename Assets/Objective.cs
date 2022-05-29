using Mirror;
using UnityEngine;

public class Objective : NetworkBehaviour
{
    public SpawnPoint spawnPoint;
    [SerializeField] private float checkRadius;

    private void Update()
    {
        CheckResource(transform.position);
    }

    private void CheckResource(Vector3 targetVector)
    {
        Collider[] targets = Physics.OverlapSphere(targetVector, checkRadius);

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].CompareTag("Resources"))
                {
                    Claim();
                    NetworkServer.Destroy(targets[i].gameObject);

                    break;
                }
            }
        }
    }

    public void Claim()
    {
        spawnPoint.hasSpawn = false;
        ResourcesManager.Instance.supplyDistributed += 1;
        NetworkServer.Destroy(gameObject);
    }
}