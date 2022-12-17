using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // variable
    WaveConfig waveConfig;
    List<Transform> waypoints = new List<Transform>();

    int wayPointIndex = 0;
    bool isMoving;

    void Update()
    {
        if (isMoving) MoveEnemy();
    }

    public void SetWaveConfig(WaveConfig waveToUse)
    {
        this.waveConfig = waveToUse;
        waypoints = waveConfig.GetWaypoints();
        ReturnToStart();
        isMoving = true;
        if (isMoving) SetEnemyActive();
    }

    public void SetEnemyActive()
    {
        Debug.Log(":::set enemy active");
        gameObject.SetActive(true);
    }

    public void ReturnToStart()
    {
        transform.position = waypoints[0].transform.position;
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
    }
}
