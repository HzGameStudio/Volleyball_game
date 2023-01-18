using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody rb;
    public float kickForce = 10f;
    public Transform cameraAngle;
    public Transform bodyAngle;

    private bool isKicked = false;
    bool onPlatform = false;


    Vector3 appliedForce;

    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!isKicked)
            {
                kickForce = collision.gameObject.GetComponent<HandsManagment>().currentKickForce;
                if (kickForce > 0)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
                appliedForce = collision.gameObject.transform.up * kickForce;
                rb.AddForce(appliedForce);
                //Debug.Log(appliedForce);
                if (appliedForce.magnitude != 0)
                {
                    isKicked = true;
                }
            }
           
        }
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        isKicked= false;
        if (collision.gameObject.CompareTag("Wall") && !onPlatform)
        {
            rb.AddForce(-1000f - transform.position.x * 28f, 1600f, transform.position.z * -20f);
            onPlatform = true;
            Invoke("NotOnPlatform", 0.1f);
        }
    }

   void NotOnPlatform()
    {
        onPlatform = false;
    }
}    
