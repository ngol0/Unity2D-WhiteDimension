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

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //for bullet
        pooledBullet = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amoutOfBullet; i++)
        {
            tmp = Instantiate(bulletToPool);
            tmp.SetActive(false);
            pooledBullet.Add(tmp);
        }

        //for laser
        pooledLaser= new List<GameObject>();
        GameObject tmp2;

        for (int i = 0; i < amoutOfLaser; i++)
        {
            tmp2 = Instantiate(laserToPool);
            tmp2.SetActive(false);
            pooledLaser.Add(tmp2);
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
}
