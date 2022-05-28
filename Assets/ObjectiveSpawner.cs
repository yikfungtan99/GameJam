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
    Transform transform;
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
                InvokeRepeating("SpawnAtSpawnPoint", 0, 2.0f);
                break;

            case SpawnMode.Random:

                foreach (Transform t in spawnPointTransform)
                {
                    spawnPoints.Add(new SpawnPoint(t));
                }

                InvokeRepeating("SpawnAtRandom", 0, 2.0f);
                break;

            default:
                break;
        }
    }

    void SpawnAtSpawnPoint()
    {
        int rand = Random.Range(0, spawnPointTransform.Count);
        Vector3 spawnPos = spawnPointTransform[rand].position;
        Spawn(prefabToSpawn, spawnPos);
    }

    void SpawnAtRandom()
    {
        SpawnInRadius(radius, avoidDistance);
    }

    void SpawnInRadius(float radius, float avoidDistance)
    {
        float randX = Random.Range(-radius, radius);
        float randY = Random.Range(-radius, radius);

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

    Vector3 PointInARadius(Vector2 center, float radiusX, float radiusY)
    {
        Vector3 pt = Vector3.zero;

        float x = center.x + (radiusX * Mathf.Cos(360));
        float y = center.y + (radiusY * Mathf.Sin(360));

        pt = new Vector3(x, 0, y);

        return pt;
    }

    private void Spawn(GameObject go, Vector3 pos)
    {
        GameObject goInstance = Instantiate(go, pos, Quaternion.identity);
        NetworkServer.Spawn(goInstance);
        spawnedTransform.Add(goInstance.transform);
    }
}
