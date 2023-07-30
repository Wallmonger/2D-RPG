using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // CTRL + R + R will rename all occurence of the word instead of doing ctrl + f
    // ALT + ENTER : Extract method to create function based on highlighted text
    // Right click and go to definition send to the function
    // ALT + ARROW UP : Put line in top

    [Header("Move info")]
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

   

    

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();

        Movement();
        CheckInput();


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

    

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
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

    private void StartAttackEvent()
    {
        if (!isGrounded)
            return;

        if (comboTimeWindow < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            // Set the decrementing timer to the selected value of dashcoolDown and apply dash
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
        
    }

    private void Movement()
    {
        // If i'm attacking, i'm not able to dash
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }

        // While dashTime don't hit 0, dash
        else if (dashTime > 0)
        {
            // While dashing, increase speed on x axis, set y to 0 to fly temporarily
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
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

    

    public void FlipController()
    {
        // Si le personnage avance vers la droite et ne regarde pas à droite
        if (rb.velocity.x > 0 && !facingRight) 
            Flip();

        // Si le personnage avance vers la gauche et regarde la droite
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
        
    }

    
}