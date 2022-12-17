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

    [Header("UI")]
    [SerializeField] UIHeart playerHeart;

    CameraShake cameraShake;
    public System.Action OnDie;

    private void Awake()
    {
        if (main != null)
        {
            cameraShake = main.GetComponent<CameraShake>();
            playerHeart.SetNumberOfHeart(health);
        }
    }

    //----BEING HIT----//
    //process hit 
    public void ProcessHit(DamageDealer damage)
    {
        health -= damage.Damage;
        Debug.Log(health);

        //destroy bullet
        damage.Hit();

        if (health <= 0)
        {
            DisplayHitAffect();
            Destroy(this.gameObject);
            OnDie?.Invoke();
        }

        if (hasCameraShake && main != null)
        {
            cameraShake.Play();
            playerHeart.OnHeartLost(health);
        }
    }

    //hit effect
    private void DisplayHitAffect()
    {
        GameObject hitEffect = Instantiate(hitVFX, transform.position, Quaternion.identity);
    }
}

