using UnityEngine;
using System.Collections;

public class obstacle_moving : MonoBehaviour {

    public float speed;
    private int check;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        check = 0;
    }

    void FixedUpdate ()
    {
	   Vector3 movement = new Vector3 (1, 0, 0);
       if (transform.position.x < 3 && check == 0){
	     rb.AddForce (movement * speed);}
       else{
            check = 1;
	     rb.AddForce (-movement * speed);
		 if(transform.position.x < -3)
            {
            check = 0;
            }   
		} 
    }
}