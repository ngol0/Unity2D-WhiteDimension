using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    [Header("Player's Bullet Pool Config")]
    public List<GameObject> pooledBullet;
    public GameObject bulletToPool;
    public int amoutOfBullet;

    [Header("Enemy's Bullet Pool Config")]
    public List<GameObject> pooledLaser;
    public GameObject laserToPool;
    public int amoutOfLaser;

    [Header("Enemy Pool Config")]
    public List<GameObject> pooledEnemy;
    public GameObject enemyToPool;
    public int amountOfEnemy = 5;

    void Awake()
    {
        SharedInstance = this;
    }

    void OnEnable()
    {
        //for bullet
        pooledBullet = new List<GameObject>();
        GameObject bullet;

        for (int i = 0; i < amoutOfBullet; i++)
        {
            bullet = Instantiate(bulletToPool);
            bullet.SetActive(false);
            pooledBullet.Add(bullet);
        }

        //for laser
        pooledLaser= new List<GameObject>();
        GameObject laser;

        for (int i = 0; i < amoutOfLaser; i++)
        {
            laser = Instantiate(laserToPool);
            laser.SetActive(false);
            pooledLaser.Add(laser);
        }

        //for enemy
        pooledLaser= new List<GameObject>();
        GameObject enemy;

        for (int i = 0; i < amountOfEnemy; i++)
        {
            enemy = Instantiate(enemyToPool);
            enemy.SetActive(false);
            pooledEnemy.Add(enemy);
        }
    }


    public GameObject GetPoolBullet()
    {
        for (int i = 0; i < amoutOfBullet; i++)
        {
            if (!pooledBullet[i].activeInHierarchy)
            {
                return pooledBullet[i];
            }
        }

        return null;
    }

    public GameObject GetPooledLaser()
    {
        for (int i = 0; i < amoutOfLaser; i++)
        {
            if (!pooledLaser[i].activeInHierarchy)
            {
                return pooledLaser[i];
            }
        }

        return null;
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < amountOfEnemy; i++)
        {
            if (!pooledEnemy[i].activeInHierarchy)
            {
                return pooledEnemy[i];
            }
        }

        return null;
    }
}
