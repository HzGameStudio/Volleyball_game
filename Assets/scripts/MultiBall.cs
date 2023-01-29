using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBall : MonoBehaviour
{
    public Rigidbody rb;
    public float kickForce = 10f;

    public bool isKicked = false;

    Vector3 starting_position;

    //public FallPointMovment fallPointMovmentScript;

    Vector3 appliedForce;

    private void Start()
    {
        starting_position = transform.position;
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("topHands"))
        {
            if (!isKicked)
            {
                kickForce = collision.gameObject.GetComponent<MultiTop>().currentKickForce;
                if (kickForce > 0)
                {
                    rb.velocity = Vector3.zero;
                    appliedForce = collision.gameObject.transform.up * kickForce;
                    rb.AddForce(appliedForce);
                    isKicked = true;
                }
                //Debug.Log(appliedForce);
                //fallPointMovmentScript.fallPoint.position = fallPointMovmentScript.GetFallPointPosotion();
            }

        }
        else if (collision.CompareTag("bottomHands"))
        {
            if (!isKicked)
            {
                kickForce = collision.gameObject.GetComponent<MultiBottom>().currentKickForce;
                if (kickForce > 0)
                {
                    rb.velocity = Vector3.zero;
                    appliedForce = collision.gameObject.transform.up * kickForce;
                    rb.AddForce(appliedForce);
                    isKicked = true;
                }
                //Debug.Log(appliedForce);  
                //fallPointMovmentScript.fallPoint.position = fallPointMovmentScript.GetFallPointPosotion();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isKicked = false;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("field"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = starting_position;

            //unityChan.GetComponent<TriggerR>().TriggerSpKick();
        }
    }

    private void Update()
    {
        if (transform.position.y < 10f)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = starting_position;
        }
    }

}
