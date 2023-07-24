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

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Holding key");
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Debug.Log("Key pressed");   
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Key released");
        }
    }
}