using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool goingRight = true;
    public Vector2 raycastDirection = new Vector2(0, 0);
    public float speed = 5f;
    private float raycastDelay = 0f;
    public float waitTime = 0f;
    private float speedupTime = 0f;


    //the layer that the raycast can hit
    public LayerMask DirectionChange;
    public LayerMask PlayerLayer;

    //get rigid body
    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //create raycasts
        RaycastHit2D changeDirectionRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, DirectionChange);
        RaycastHit2D changeDirectionLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, DirectionChange);
        RaycastHit2D playerDetectedLeft = Physics2D.Raycast(transform.position, Vector2.left, 3f, PlayerLayer);
        RaycastHit2D playerDetectedRight = Physics2D.Raycast(transform.position, Vector2.right, 3f, PlayerLayer);

        //Detect right side and do actions
        if (changeDirectionRight && raycastDelay <=0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            goingRight = !goingRight;
            raycastDelay = 2f;
            waitTime = Random.Range(1, 2); ;
        }
        //Detect left side and do actions
        if (changeDirectionLeft && raycastDelay <= 0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            goingRight = !goingRight;
            raycastDelay = 2f;
            waitTime = Random.Range(1, 2);
        }

        //Change direction
        if (goingRight == true && waitTime <= 0)
        {
            _rigidbody.velocity = new Vector2(1 * speed, _rigidbody.velocity.y);
            raycastDirection = new Vector2(0, 1);
        }
        if (goingRight == false && waitTime <= 0)
        {
            _rigidbody.velocity = new Vector2(-1 * speed, _rigidbody.velocity.y);
            raycastDirection = new Vector2(1, 0);
        }

        //Detect player
        if (playerDetectedLeft && speedupTime <= 0)
        {
            waitTime -= 1;
            speedupTime = 4f;
        }
        if (playerDetectedRight && speedupTime <= 0)
        {
            waitTime -= 1;
            speedupTime = 4f;
        }

        //Delay on raycast
        raycastDelay -= Time.deltaTime;
        waitTime -= Time.deltaTime;
        speedupTime -= Time.deltaTime;

        //regulate speed
        if (speedupTime >= 0)
        {
            speed = 8f;
        }
        else
        {
            speed = 5f;
        }


    }
}
