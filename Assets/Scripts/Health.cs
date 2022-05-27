using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 500;
    [SerializeField] GameObject hitVFX;

    //----BEING HIT----//
    //process hit 
    public void ProcessHit(DamageDealer damage)
    {
        DisplayHitAffect();
        health -= damage.Damage;

        //destroy bullet
        damage.Hit();

        if (health <= 0)
        {
            //destroy player if health <= 0
            Destroy(this.gameObject);
        }
    }

    //hit effect
    private void DisplayHitAffect()
    {
        GameObject hitEffect = Instantiate(hitVFX, transform.position, Quaternion.identity);
    }
}
