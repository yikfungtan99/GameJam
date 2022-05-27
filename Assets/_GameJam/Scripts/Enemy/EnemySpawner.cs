using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform pathMinX;
    [SerializeField] private Transform pathMaxX;

    [SerializeField] private float enemySpawnMinDistance;

    [SerializeField] private int spawnNum;

    private List<Transform> enemyTransformList = new List<Transform>();

    private float enemySpawnDistance;

    private void Start()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.Spawn(enemyGO);
    }

    private float GetSpawnDist()
    {
        return pathMaxX.position.x - Mathf.Abs(pathMinX.position.x);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnNum; i++)
        {
            SpawnRandomAlongPath(enemyPrefab, enemySpawnDistance, out GameObject enemyInstance);
        }
    }

    //Naive: use spline points when spline points are implemented
    private bool SpawnRandomAlongPath(GameObject prefab, float spawnDistance, out GameObject enemyInstance)
    {
        bool canSpawn = false;
        enemyInstance = null;

        float randX = Random.Range(-spawnDistance, spawnDistance);
        Vector2 spawnPos = new Vector2(randX, pathMinX.position.y);
        Debug.Log("0");
        if(enemyTransformList.Count > 0)
        {
            for (int i = 0; i < enemyTransformList.Count; i++)
            {
                float dist = Vector2.Distance(enemyTransformList[i].position, spawnPos);

                if (dist > enemySpawnMinDistance)
                {
                    Spawn(prefab, out enemyInstance, out canSpawn, spawnPos);
                }
            }
        }
        else
        {
            Spawn(prefab, out enemyInstance, out canSpawn, spawnPos);
        }

        return canSpawn;
    }

    private void Spawn(GameObject objToSpawn, out GameObject enemyInstance, out bool canSpawn, Vector2 spawnPos)
    {
        Debug.Log("Spawn");
        enemyInstance = GameObject.Instantiate(objToSpawn, spawnPos, Quaternion.identity);
        enemyTransformList.Add(enemyInstance.transform);
        canSpawn = true;
    }
}
