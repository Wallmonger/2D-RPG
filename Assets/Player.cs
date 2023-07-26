using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ALT + ENTER : Extract method to create function based on highlighted text
    // Right click and go to definition send to the function
    // ALT + ARROW UP : Put line in top


    private float xInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private Animator anim;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        Movement();
        CheckInput();
        AnimatorControllers();
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
        rb.velocity += new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers () 
    {
        // If player is moving, we set the boolean value to trigger the animation
        bool isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
    }
}