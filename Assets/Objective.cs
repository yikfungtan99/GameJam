using Mirror;
using UnityEngine;

public class Objective : NetworkBehaviour
{
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
        Debug.Log("Claimed!");
        NetworkServer.Destroy(gameObject);
    }
}