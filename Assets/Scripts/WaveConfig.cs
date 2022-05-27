using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfig : ScriptableObject
{

    //Properties
    [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    [field: SerializeField] public GameObject PathPrefab { get; private set; }
    [field: SerializeField] public float timeBetweenSpawns { get; private set; } = 0.5f;
    [field: SerializeField] public float spawnRandomFactor { get; private set; } = 0.3f;
    [field: SerializeField] public float minSpawnTime { get; private set; } = 0.2f;
    [field: SerializeField] public int numberOfEnemies { get; private set; } = 5;
    [field: SerializeField] public float moveSpeed { get; private set; } = 2f;

    // Getting all the waypoints
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform wayPoint in PathPrefab.transform)
        {
            waveWaypoints.Add(wayPoint);
        }

        return waveWaypoints;
    }


    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenSpawns - spawnRandomFactor, timeBetweenSpawns + spawnRandomFactor);

        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }


}
