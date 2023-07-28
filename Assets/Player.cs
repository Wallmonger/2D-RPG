using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // CTRL + R + R will rename all occurence of the word instead of doing ctrl + f
    // ALT + ENTER : Extract method to create function based on highlighted text
    // Right click and go to definition send to the function
    // ALT + ARROW UP : Put line in top
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;

    [Header("Attack info")]
    [SerializeField] private float comboTime = .3f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int comboCounter;

    private float xInput;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;    
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();


        // Decrement dashTime overtime to create a cooldown
        dashTime -= Time.deltaTime; 
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

        FlipController();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;

        if (comboCounter > 2)
            comboCounter = 0;
        
    }

    private void CollisionChecks()
    {
        // Cast a ray against colliders in the scene (gameObject position, direction, max distance, LayerMask)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (comboTimeWindow < 0)
                comboCounter = 0;

            isAttacking = true;
            comboTimeWindow = comboTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }


        // DashTime will add positive values to the duration, and until it reach 0 again, we'll dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0)
        {
            // Set the decrementing timer to the selected value of dashcoolDown and apply dash
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
        
    }

    private void Movement()
    {
        // While dashTime don't hit 0, dash
        if (dashTime > 0)
        {
            // While dashing, increase speed on x axis, set y to 0 to fly temporarily
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        } 
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }


    }

    private void Jump()
    {
        // Allowing jump only if character's gizmo hit the floor
        if (isGrounded)
        rb.velocity += new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers () 
    {
        // If player is moving, we set the boolean value to trigger the animation
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);

        // Trigger animation when dashTime > 0
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }

    private void Flip()
    {
        // facingDir sera multiplié par -1 à chaque appel, ce qui aura pour effet de changer la direction automatiquement ( -1 * -1 = 1) 
        facingDir = facingDir * -1;

        // On inverse le booléen
        facingRight = !facingRight;      
        transform.Rotate(0, 180, 0);
    }

    public void FlipController()
    {
        // Si le personnage avance vers la droite et ne regarde pas à droite
        if (rb.velocity.x > 0 && !facingRight) 
            Flip();

        // Si le personnage avance vers la gauche et regarde la droite
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
        
    }

    private void OnDrawGizmos()
    {
        // Drawing a line to the feet of the character
        Gizmos.DrawLine(transform.position, new Vector3 (transform.position.x, transform.position.y - groundCheckDistance));
    }
}