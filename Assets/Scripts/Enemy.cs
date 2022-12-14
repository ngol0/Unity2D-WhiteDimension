using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Shoot Config")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 10f;

    // for animation
    bool active = true;
    Animator animator;

    // cache ref
    SpriteRenderer spriteR;

    //for shooting condition
    bool hasBeenShot = false;


    // Start is called before the first frame update
    void Start()
    {
        // animation
        animator = this.gameObject.GetComponent<Animator>();

        // shot counts
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        // sprite
        spriteR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenShot) CountDownAndShoot();
    }
    
    //----SHOOTING----//
    private void CountDownAndShoot()
    {
        // count down
        shotCounter -= Time.deltaTime;

        //shoot
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        float paddingX = 3*(spriteR.bounds.extents.x)/4;
        float paddingY = 3*(spriteR.bounds.extents.y)/4;


        //laser from the right
        Vector3 positionRight = new Vector3(transform.position.x + paddingX, transform.position.y - paddingY, transform.position.z);
        //GameObject laserCloneRight = Instantiate(laserPrefabs, positionRight, Quaternion.identity) as GameObject;
        GameObject laserCloneRight = ObjectPool.SharedInstance.GetPooledLaser();

        if (laserCloneRight != null)
        {
            laserCloneRight.transform.position = positionRight;
            laserCloneRight.transform.rotation = Quaternion.identity;
            laserCloneRight.SetActive(true);
        }


        //laser from left
        Vector3 positionLeft = new Vector3(transform.position.x - paddingX, transform.position.y - paddingY, transform.position.z);
        //GameObject laserCloneLeft = Instantiate(laserPrefabs, positionLeft, Quaternion.identity) as GameObject;
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

            Health health = GetComponent<Health>();

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

            hasBeenShot = true;
        }      
    }

    // restart animation after hit
    IEnumerator ResetTarget()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("isHit", false);
        active = true;
    }
}
