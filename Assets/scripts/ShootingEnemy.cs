using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{   
    public Rigidbody2D rb;
    public float enemySpeed;
    public Transform target;
    public float minimumDistance;
    

    public GameObject projectile;
    public float timeBetweenShots;
    private float nextShotTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextShotTime = Time.time + timeBetweenShots;
        }

        if (Vector2.Distance(transform.position, target.position) > minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -enemySpeed * Time.deltaTime);
            //transform.position = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

    }

}
