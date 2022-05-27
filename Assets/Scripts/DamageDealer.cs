using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // properties
    [field: SerializeField] public int Damage { get; private set; } = 200;

    public void Hit()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }


}
