using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // variable
    WaveConfig waveConfig;
    List<Transform> waypoints;

    int wayPointIndex = 0;


    // properties


    // Get the waypoints 
    void Start()
    { 
        waypoints = waveConfig.GetWaypoints();
    }

  
    void Update()
    {
        MoveEnemy();
    }


    public void SetWaveConfig(WaveConfig waveToUse)
    {
        this.waveConfig = waveToUse;
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
            Destroy(gameObject);
        }
    }
}
