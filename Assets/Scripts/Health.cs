using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 500;
    [SerializeField] private GameObject hitVFX;

    [Header("Camera Shake")]
    [SerializeField] private bool hasCameraShake;
    [SerializeField] private Camera main;

    CameraShake cameraShake;

    private void Awake() {
        if (main != null)
            cameraShake = main.GetComponent<CameraShake>();
    }

    //----BEING HIT----//
    //process hit 
    public void ProcessHit(DamageDealer damage)
    {
        health -= damage.Damage;

        //destroy bullet
        damage.Hit();

        if (health <= 0)
        {
            DisplayHitAffect();
            Destroy(this.gameObject);
        }

        if (hasCameraShake && main!=null)
            cameraShake.Play();
    }

    //hit effect
    private void DisplayHitAffect()
    {
        GameObject hitEffect = Instantiate(hitVFX, transform.position, Quaternion.identity);
    }
}
