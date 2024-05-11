using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    private Rigidbody controller;
    private Vector3 moveVector;
    public float accelSpeed;
    private int jump;

    private bool isDead = false;

    public AudioSource sceneAudio;
 
    void Start () {
        controller = GetComponent<Rigidbody>();
    }
	

	void Update () {

        if (isDead)
            return;





        if (controller.transform.position.y < 0.6)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = 85;
            }
        }
        else
        {
            jump = -5;
        }

        



        float moveHorizontal = Input.GetAxis("Horizontal");

        controller.velocity = new Vector3(moveHorizontal * 20, jump, accelSpeed * 20);
	}

    


    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.name == "Enemy")   
        {
            Death();
        }
        if (hit.gameObject.name == "Enemy1")   
        {
            Death();
        }
        if (hit.gameObject.name == "Enemy2")  
        {
            Death();
        }
        if (hit.gameObject.name == "Enemy3")   
        {
            Death();
        }
        if (hit.gameObject.name == "Enemy4")  
        {
            Death();
        }
        if (hit.gameObject.name == "Surprise")   
        {
            hit.gameObject.SetActive(false);
        }

    }

    private void Death()
    {
        isDead = true;
        sceneAudio.Stop();
        GetComponent<PlanesPassed>().OnDeath(); 
    }


    public IEnumerator accelEnd()
    {

        yield return new WaitForSeconds(3.0f);
        accelSpeed = 0.7f; 
    }

    

}
