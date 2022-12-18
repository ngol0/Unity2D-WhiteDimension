using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Shoot Config")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 10f;

    [Header("UI")]
    [SerializeField] Sprite onWinSprite;

    // for animation
    bool active = true;
    Animator animator;

    // cache ref
    SpriteRenderer spriteR;
    Health health;


    // Start is called before the first frame update
    void Start()
    {
        // animation
        animator = this.gameObject.GetComponent<Animator>();

        // shot counts
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        // sprite
        spriteR = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();

        if (health) health.OnDie += OnScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.enemyStartShooting) CountDownAndShoot();
    }
    
    //----SHOOTING----//
    private void CountDownAndShoot()
    {
        // count down
        shotCounter -= Time.deltaTime;

        //shoot
        if (shotCounter <= 0f)
        {
            StartCoroutine(Fire());
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private IEnumerator Fire()
    {
        float paddingX = 3*(spriteR.bounds.extents.x)/4;
        float paddingY = 3*(spriteR.bounds.extents.y)/4;


        //laser from the right
        Vector3 positionRight = new Vector3(transform.position.x + paddingX, transform.position.y - paddingY, transform.position.z);
        GameObject laserCloneRight = ObjectPool.SharedInstance.GetPooledLaser();

        if (laserCloneRight != null)
        {
            laserCloneRight.transform.position = positionRight;
            laserCloneRight.transform.rotation = Quaternion.identity;
            laserCloneRight.SetActive(true);
        }

        yield return new WaitForSeconds(0.05f);

        //laser from left
        Vector3 positionLeft = new Vector3(transform.position.x - paddingX, transform.position.y - paddingY, transform.position.z);
        GameObject laserCloneLeft = ObjectPool.SharedInstance.GetPooledLaser();
        if (laserCloneLeft != null)
        {
            laserCloneLeft.transform.position = positionLeft;
            laserCloneLeft.transform.rotation = Quaternion.identity;
            laserCloneLeft.SetActive(true);
        }

    }

    //------BEING HIT-----//
    private void OnTriggerEnter2D(Collider2D other)
    {

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        // animation of enemy when hit
        if (other.gameObject.tag == "Bullet")
        {
            if (health)
            {
                health.ProcessHit(damageDealer);
            }

            if (active == true)
            {
                animator.SetBool("isHit", true);
                StartCoroutine(ResetTarget());
                active = false;
            }      
        }      
    }

    private void OnScore()
    {
        GameManager.Instance.OnGetScore();
    }

    // restart animation after hit
    IEnumerator ResetTarget()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isHit", false);
        active = true;
    }

}
