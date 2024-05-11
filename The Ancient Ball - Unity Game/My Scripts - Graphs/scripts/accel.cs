using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accel : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            SphereMovement playerScript = other.gameObject.GetComponent<SphereMovement>(); 
            if (playerScript)
            {
                playerScript.accelSpeed = 1.2f;

                StartCoroutine(playerScript.accelEnd());

            }
        }
        else if (other.tag == "AI")
        {
            EnemyAI playerScript = other.gameObject.GetComponent<EnemyAI>(); 
            if (playerScript)
            {
                playerScript.accelSpeed = 1.5f;

                StartCoroutine(playerScript.accelEnd2());

            }
        }




        if (other.tag == "Player")
        {
            SphereMovement1 playerScript = other.gameObject.GetComponent<SphereMovement1>(); 
            if (playerScript)
            {
                playerScript.accelSpeed = 1.5f;

                StartCoroutine(playerScript.accelEnd1());

            }
        }
    }


   
    




    }
