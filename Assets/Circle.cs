using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ### Derived Class (extends)
// Instead of inheriting from MonoBehaviour which contains all functions relative to unity, we inherit from Shape.
// Shape has the monoBehaviour himself, so we can still access the functions made by unity

public class Circle : Shape
{   
    // Getting the Start() Method from origin class
    protected override void Start()
    {
        // base.Start() is calling the function Start() from inherited class Shape (base)
        base.Start();

        // Polymorphism allows to change features from the base Class, and making them act differently
        Debug.Log("i am inherited from shape");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
