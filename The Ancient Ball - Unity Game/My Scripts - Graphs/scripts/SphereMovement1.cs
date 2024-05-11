using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereMovement1 : MonoBehaviour
{
    private Rigidbody controller;
    private Vector3 moveVector;

    public float accelSpeed;
    public Transform[] target;
    public int enemysCount;
    private int enemysWining;
    public Text score2Text;
    private bool isDead = false;
    private int Pos;
    public Text score3Text;
    public Text score4Text;
    private int jump;

    public AudioSource sceneAudio;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {

       
        score2Text.text = ((int)Pos).ToString();

        if (isDead==false)
        {
            enemysWining = 0;
            for (int i = 0; i < enemysCount; i++)
            {
                        if (transform.position.z < target[i].position.z)
                        {
                           enemysWining++;
                        }
                        Pos = enemysWining + 1 ;
            }

        }



        if (isDead)
        {
            score3Text.text = Pos.ToString();
            score4Text.text = Pos.ToString();
            return;
        }
            


        if (transform.position.z > 1030)
        {
          isDead = true;
          score3Text.text = Pos.ToString();
          Death1();
        }
        


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
            Death1();
        }
        if (hit.gameObject.name == "Enemy1")  
        {
            Death1();
        }
        if (hit.gameObject.name == "Enemy2")   
        {
            Death1();
        }
        if (hit.gameObject.name == "Enemy3")   
        {
            Death1();
        }
        if (hit.gameObject.name == "Enemy4")   
        {
            Death1();
        }
        if (hit.gameObject.name == "Surprise")  
        {
            hit.gameObject.SetActive(false);
        }

    }



    private void Death1()
    {
        isDead = true;
        sceneAudio.Stop();
        if (Pos == 1 && transform.position.z > 1030)
        {
         GetComponent<PlanesPassed1>().OnWin();
        }
        else
        { 
         GetComponent<PlanesPassed1>().OnDeath1();
        }
        
    }



    public IEnumerator accelEnd1()
    {

        yield return new WaitForSeconds(3.0f);
        accelSpeed = 0.8f; 
    }


}