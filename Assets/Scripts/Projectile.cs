using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // params
    [SerializeField] float speed = 5f;

    [SerializeField] int direction;

    // caches references
    Rigidbody2D physics;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot(this.direction);
    }

    private void Shoot(int direction)
    {
        physics.velocity = new Vector2(0, speed * direction);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

}
