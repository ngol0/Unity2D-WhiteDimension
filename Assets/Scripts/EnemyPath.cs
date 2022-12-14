using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // variable
    WaveConfig waveConfig;
    List<Transform> waypoints = new List<Transform>();

    int wayPointIndex = 0;
    bool isMoving = false;


    // properties


    // Get the waypoints 
    void Start()
    {
        foreach (var waypoint in waveConfig.GetWaypoints())
        {
            waypoints.Add(waypoint);
        }
    }


    void Update()
    {
        MoveEnemy();
    }


    public void SetWaveConfig(WaveConfig waveToUse)
    {
        Debug.Log(":::calling wave config");
        this.waveConfig = waveToUse;
        waypoints.Clear();
        foreach (var waypoint in waveConfig.GetWaypoints())
        {
            waypoints.Add(waypoint);
        }
    }

    // move enemy
    private void MoveEnemy()
    {
        if (wayPointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[wayPointIndex].transform.position;
            var movementSpeed = waveConfig.moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementSpeed);

            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            gameObject.SetActive(false);
            Debug.Log("Reusing:::");
        }
    }
}
