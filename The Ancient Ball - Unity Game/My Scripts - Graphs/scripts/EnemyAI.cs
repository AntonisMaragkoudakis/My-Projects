using UnityEngine;
using System.Collections;
public class EnemyAI : MonoBehaviour
{

    public float accelSpeed;
    public RaycastHit hit1;
    public RaycastHit hit2;
    public RaycastHit hit3;
    public RaycastHit hit4;
    public RaycastHit hit5;
    private Vector3 moveVector;
    private Rigidbody controller;
    private int range = 5;
    private GameObject kati;
    private float moveHorizontal;
    private int rand;
    private int jump;


    void Start()
    {
        accelSpeed = 0.65f;
        controller = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(9, 9);
        controller.velocity = new Vector3(0, 0, 0);
        moveHorizontal = controller.velocity.x;

    }

    void Update()
    {


        Transform ray = transform;

        if ((Physics.Raycast(transform.position + (transform.right * 0.49f), transform.forward, out hit1, range)) && (Physics.Raycast(transform.position - (transform.right * 0.49f), transform.forward, out hit2, range)) && (hit1.collider.gameObject.CompareTag("Enemy") || hit2.collider.gameObject.CompareTag("Enemy"))) // && hit.collider.gameObject.CompareTag("Enemy")
        {
            rand = Random.Range(0, 2);

            if (rand == 0)
            {
                moveHorizontal = -1.5f;
            }
            else
            {
                moveHorizontal = 1.5f;
            }



        }
        else if ((Physics.Raycast(transform.position + (transform.right * 0.49f), transform.forward, out hit1, range)) && (!(Physics.Raycast(transform.position - (transform.right * 0.49f), transform.forward, out hit2, range))) && hit1.collider.gameObject.CompareTag("Enemy"))
        {
            moveHorizontal = -1.5f;
        }
        else if ((!(Physics.Raycast(transform.position + (transform.right * 0.49f), transform.forward, out hit1, range))) && (Physics.Raycast(transform.position - (transform.right * 0.49f), transform.forward, out hit2, range)) && hit2.collider.gameObject.CompareTag("Enemy"))
        {
            moveHorizontal = 1.5f;
        }
        else
        {
            moveHorizontal = 0.0f;
        }



        if ((Physics.Raycast(transform.position + (transform.right * 0.49f), transform.forward, out hit3, range)) && (Physics.Raycast(transform.position - (transform.right * 0.49f), transform.forward, out hit4, range)) && (hit3.collider.gameObject.CompareTag("moving") || hit4.collider.gameObject.CompareTag("moving"))) // && hit.collider.gameObject.CompareTag("Enemy")
        {


            if (transform.position.x > 0)
            {
                moveHorizontal = -4.5f;
            }
            else
            {
                moveHorizontal = 4.5f;
            }

        }

        if ((Physics.Raycast(transform.position, transform.forward, out hit5, 1.0f)) && (hit5.collider.gameObject.CompareTag("jump")))
        {
            if (controller.transform.position.y < 0.6)
            {
                jump = 85;
            }
        }
        else
        {
            jump = -5;
        }

        controller.velocity = new Vector3(moveHorizontal * 20, jump, accelSpeed * 20);

        //Debug.DrawRay(transform.position + (transform.right * 0.45f), transform.forward * range, Color.red);

    }




    private void OnCollisionEnter(Collision hit)
    {
        if ((hit.gameObject.tag == "Enemy") || (hit.gameObject.tag == "moving") || (hit.gameObject.tag == "jump") || (hit.gameObject.tag == "left") || (hit.gameObject.tag == "right") || (hit.gameObject.tag == "center"))
        {
            gameObject.SetActive(false);
        }
    }


    public IEnumerator accelEnd2()
    { 
        yield return new WaitForSeconds(3.0f);
        accelSpeed = 0.65f; 
    }


}