using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnMode
{
    SpawnPoint,
    Random
}

public class SpawnPoint
{
    public Transform transform;
    public bool hasSpawn = false;

    public SpawnPoint(Transform transform)
    {
        this.transform = transform;
    }
}

public class ObjectiveSpawner : NetworkBehaviour
{
    [SerializeField] private SpawnMode spawnMode;
    [SerializeField] private GameObject prefabToSpawn;

    [SerializeField] private Transform center;

    [SerializeField] List<Transform> spawnPointTransform = new List<Transform>();
    List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    [SerializeField] private float radius;
    [SerializeField] private float avoidDistance;

    private List<Transform> spawnedTransform = new List<Transform>();

    private void Start()
    {
        switch (spawnMode)
        {
            case SpawnMode.SpawnPoint:

                foreach (Transform t in spawnPointTransform)
                {
                    spawnPoints.Add(new SpawnPoint(t));
                }

                InvokeRepeating("SpawnAtSpawnPoint", 0, 2.0f);
                break;

            case SpawnMode.Random:

                InvokeRepeating("SpawnAtRandom", 0, 2.0f);
                break;

            default:
                break;
        }
    }

    void SpawnAtSpawnPoint()
    {
        int rand = Random.Range(0, spawnPoints.Count);

        if (!spawnPoints[rand].hasSpawn)
        {
            Vector3 spawnPos = spawnPoints[rand].transform.position;

            spawnPoints[rand].hasSpawn = true;
            GameObject go = Spawn(prefabToSpawn, spawnPos);

            Objective obj = go.GetComponent<Objective>();

            if(obj != null)
            {
                obj.spawnPoint = spawnPoints[rand];
            }
        }
    }

    void SpawnAtRandom()
    {
        SpawnInRadius(radius, avoidDistance);
    }

    void SpawnInRadius(float radius, float avoidDistance)
    {
        Vector2 randomPoint = new Vector2(center.position.x, center.position.z) + Random.insideUnitCircle * radius * 0.5f;

        Vector3 spawnPos = new Vector3(randomPoint.x, center.position.y, randomPoint.y);

        if (spawnedTransform.Count > 0)
        {
            for (int i = 0; i < spawnedTransform.Count; i++)
            {
                if (Vector3.Distance(spawnPos, spawnedTransform[i].position) > avoidDistance)
                {
                    Spawn(prefabToSpawn, spawnPos);
                    break;
                }
            }
        }
        else
        {
            Spawn(prefabToSpawn, spawnPos);
        }
    }

    private GameObject Spawn(GameObject go, Vector3 pos)
    {
        GameObject goInstance = Instantiate(go, pos, Quaternion.identity);
        NetworkServer.Spawn(goInstance);
        spawnedTransform.Add(goInstance.transform);

        return goInstance;
    }
}
