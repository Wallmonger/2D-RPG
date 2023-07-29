using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ### Origin class


public class Shape : MonoBehaviour
{
    
    public string shapeName;
    public Rigidbody2D rb;
    public Vector2 velocity;

    // Event function can be inherited from another place only if it's public or protected (non visible but inheritable)
    // To be inheritable, the function needs the keyword "virtual", which means it can be overridden by a derived class

    protected virtual void Start()
    {
        Debug.Log("This shape is " + shapeName);
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
