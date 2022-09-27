using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool goingRight = true;
    private bool rayDirection = true;
    private float speed = 5f;
    private float raycastDelay = 0f;
    public float waitTime = 0f;

    //the layer that the raycast can hit
    public LayerMask DirectionChange;

    //get rigid body
    private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D changeDirectionRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, DirectionChange);
        RaycastHit2D changeDirectionLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, DirectionChange);


        //Detect right side and do actions
        if (changeDirectionRight && raycastDelay <=0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            goingRight = !goingRight;
            raycastDelay = 3f;
            waitTime = Random.Range(1, 3); ;
        }
        //Detect left side and do actions
        if (changeDirectionLeft && raycastDelay <= 0)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            goingRight = !goingRight;
            raycastDelay = 3f;
            waitTime = Random.Range(1, 3);
        }

        //Change direction
        if (goingRight == true && waitTime <= 0)
        {
            _rigidbody.velocity = new Vector2(1 * speed, _rigidbody.velocity.y);
        }
        if (goingRight == false && waitTime <= 0)
        {
            _rigidbody.velocity = new Vector2(-1 * speed, _rigidbody.velocity.y);
        }

        //Delay on raycast
        raycastDelay -= Time.deltaTime;
        waitTime -= Time.deltaTime;

    }

}
