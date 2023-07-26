using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ALT + ENTER : Extract method to create function based on highlighted text
    // Right click and go to definition send to the function
    // ALT + ARROW UP : Put line in top
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

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
        FlipController();
        AnimatorControllers();
    }

    private void CollisionChecks()
    {
        // Cast a ray against colliders in the scene (gameObject position, direction, max distance, LayerMask)
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
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

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
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