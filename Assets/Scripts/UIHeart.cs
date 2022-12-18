using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeart : MonoBehaviour
{
    int heartIndex = 0;
    int numberOfHeart;

    [SerializeField] GameObject heartPrefab;

    public void SetNumberOfHeart(int health)
    {
        numberOfHeart = (int)health / 100;
    }

    private void Start()
    {
        InitHeart();
    }

    private void InitHeart()
    {
        for (int i = 0; i < numberOfHeart; i++)
        {
            var heart = Instantiate(heartPrefab, transform);
        }
    }

    public void OnHeartLost(int health)
    {
        int currentHeart = (int)health / 100;
        var norDmg = numberOfHeart - currentHeart;
        int heartToDestroy;

        numberOfHeart = currentHeart;
        heartToDestroy = (norDmg > transform.childCount) ? transform.childCount : norDmg;

        for (int i = 0; i < heartToDestroy; i++)
        {
            var child = transform.GetChild(i).gameObject;
            if (child != null)
                Destroy(child);
        }
    }
}
