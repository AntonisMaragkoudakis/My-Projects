using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_1 : MonoBehaviour
{
    public float speed;
    private int check;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        check = 1;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(1, 0, 0);
        if (transform.position.x > -3 && check == 1)
        {
            rb.AddForce(-movement * speed);
        }
        else
        {
            check = 0;
            rb.AddForce(movement * speed);
            if (transform.position.x > 3)
            {
               check = 1;
            }      
        }
    }
}