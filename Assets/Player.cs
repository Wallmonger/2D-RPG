using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float xInput;
    [SerializeField] private float moveSpeed;
    private float jumpForce;
    private Rigidbody2D rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
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