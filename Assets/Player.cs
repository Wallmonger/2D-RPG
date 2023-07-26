using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float xInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private bool isMoving;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);



        if (Input.GetButtonDown("Jump")) 
        {
            rb.velocity += new Vector2(rb.velocity.x, jumpForce);
        }

        xInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(xInput);

        // If player is moving, we set the boolean value to trigger the animation
        isMoving = rb.velocity.x != 0;

        anim.SetBool("isMoving", isMoving);
        
    }
}