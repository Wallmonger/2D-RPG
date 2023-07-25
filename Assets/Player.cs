using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float xInput;
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    

    void Start()
    {
        
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
    }
}