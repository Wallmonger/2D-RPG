using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    


    void Start()
    {

    }


    void Update()
    {
        if (Input.GetButtonDown("Jump")) 
        {
            Debug.Log("jumping");
        }

        Debug.Log(Input.GetAxisRaw("Horizontal"));
    }
}