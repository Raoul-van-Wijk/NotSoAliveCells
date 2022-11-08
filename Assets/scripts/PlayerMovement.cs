using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private float wallJumpingPower = 20f;

    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private readonly float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private bool isTouchingLeft;
    private bool isTouchingRight;
    private bool wallJumping;
    private float touchingLeftOrRight;

    private bool isHit = false;
    [SerializeField] private float knockbackStrengthX = 7f;
    [SerializeField] private float knockbackStrengthY = 7f;
    [SerializeField] private float resetTimer = 0.5f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private AudioClip jumpSound, dashSound;

    private PlayerInput playerInput;

    private bool isHoldingJumpButton;

	void Start()
	{  
        playerInput = GetComponent<PlayerInput>();
        
	}

	void Update()
    {
		horizontal = playerInput.actions.FindAction("Movement").ReadValue<float>();

        // checks if player is on the ground, coyotetimecounter allows player to jump shortly after no longer touching ground if it's within [coyoteTime]
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            AudioManager.Instance.PlaySound(jumpSound);

            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }   

        if (isHoldingJumpButton)
        {

            jumpBufferCounter -= Time.deltaTime;

        } else
        {
            jumpBufferCounter = 0;
        }



        WallJumping();

        Flip();
    }

	private void FixedUpdate()
    {
        if (isDashing || isHit) {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void Dash(InputAction.CallbackContext context)
	{
        if (context.performed && canDash)
            StartCoroutine(DashMove());
	}

    private IEnumerator DashMove()
	{
        AudioManager.Instance.PlaySound(dashSound);
        canDash = false;
        isDashing = true;
        PlayerController pc = FindObjectOfType<PlayerController>();
        pc.SetImmortal(true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        pc.SetImmortal(false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHoldingJumpButton = true;
            jumpBufferCounter = jumpBufferTime;
            
            if ((isTouchingRight || isTouchingLeft) && !IsGrounded())
                rb.velocity = new Vector2(speed * touchingLeftOrRight, wallJumpingPower);
        }
        if(context.canceled && rb.velocity.y > 0f)
        {
            isHoldingJumpButton = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    /// <summary>
    /// Knocks the player back reverse from where the origin comes from
    /// </summary>
    /// <param name="origin">Object from which the knockback comes from</param>
    public void Knockback(GameObject origin)
	{
        isHit = true;

        // reset velocity so player knockback is always consistent regardless of previous velocity
        rb.velocity = Vector2.zero;

        // determine where the player needs to be knocked back towards, determined by where the player is hit from
        if (transform.position.x - origin.transform.position.x > 0)
        {
            rb.AddForce(new Vector2(knockbackStrengthX, knockbackStrengthY), ForceMode2D.Impulse);
        } else
		{
            rb.AddForce(new Vector2(-knockbackStrengthX, knockbackStrengthY), ForceMode2D.Impulse);
        }
        StartCoroutine(ResetKnockback());
    }

    // Timer for when the player is allowed to move again
    private IEnumerator ResetKnockback()
	{
        yield return new WaitForSeconds(resetTimer);
        isHit = false;
	}

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void WallJumping()
    {
        IsGrounded();

        isTouchingLeft = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y),
            new Vector2(0.2f, 0.9f), 0f, groundLayer);

        isTouchingRight = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y),
            new Vector2(0.2f, 0.9f), 0f, groundLayer);

        if (isTouchingLeft)
        {
            touchingLeftOrRight = 1;
        }
        else if (isTouchingRight)
        {
            touchingLeftOrRight = -1;
        }
    }
}
