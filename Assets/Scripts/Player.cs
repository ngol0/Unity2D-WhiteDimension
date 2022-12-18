using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // variables
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Bullet")]
    [SerializeField] float FiringPeriod = 0.4f;


    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // cache reference
    SpriteRenderer sprite;
    Coroutine firingCoroutine;
    Health health;


    // Start is called before the first frame update
    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        health  = GetComponent<Health>();

        if (health) health.OnDie += OnPlayerDie;

        //set boundary of space to move
        SetUpMoveSpace();

    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Fire();
        
    }


    //----SHOOTING----//
    private void Fire()
    {
        //getbuttondown will true only once when pressed
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }


    // coroutine to shoot continuously
    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject bulletClone = ObjectPool.SharedInstance.GetPoolBullet();

            if (bulletClone != null)
            {
                bulletClone.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);
                bulletClone.transform.rotation = Quaternion.identity;
                bulletClone.SetActive(true);

            }

            yield return new WaitForSeconds(FiringPeriod);
        }

    }


    //----MOVING----//
    //restricted space to move
    private void SetUpMoveSpace()
    {
        Camera gameCamera = Camera.main;

        float paddingX = sprite.bounds.extents.x;
        float paddingY = sprite.bounds.extents.y;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + paddingX;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - paddingX;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + paddingY;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - paddingY;
    }



    // moving when user presses key
    private void Move()
    {
        
        // player's position
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.y);

        // When player press keys that move Horizontally
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        playerPos.x = Mathf.Clamp(playerPos.x + deltaX, xMin, xMax);
        
        // Move Vertically
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        playerPos.y = Mathf.Clamp(playerPos.y + deltaY, yMin, yMax);

        // Update position
        transform.position = playerPos;
    }


    //-----BEING HIT-----//
    //when enemy's bullet hit player
    private void OnTriggerEnter2D(Collider2D other)
    {
        //ref to bullet & enemy
        DamageDealer damage = other.GetComponent<DamageDealer>();

        if (damage != null)
        {
            if (health)
            {
                health.ProcessHit(damage);
            }
        }      
    }

    private void OnPlayerDie()
    {
        GameManager.Instance.OnLoseGame();
    }
}

