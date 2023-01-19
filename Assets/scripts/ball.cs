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

    bool onPlatform = false;

    public float xMin = -25f;
    public float xMax = -15f;
    public float yMin = -20f;
    public float yMax = 20f;
    public float maxHeight = 50f;

    float gravity = 30f;
    float xStart;
    float zStart;
    float xEnd;
    float zEnd;
    float flyingTime;
    float vX;
    float vY;
    float vZ;

    private void OnCollisionEnter(Collision collision)
    {
        isKicked= false;
        if (collision.gameObject.CompareTag("Wall") && !onPlatform)
        {

            xStart = transform.position.x;
            zStart = transform.position.z;
            xEnd = Random.Range(xMin, xMax);
            zEnd = Random.Range(yMin, yMax);

            flyingTime = Mathf.Sqrt(2 * maxHeight / gravity) * 2f;
            Debug.Log(flyingTime);

            vY = gravity * flyingTime / 2;
            vX = (xEnd - xStart) / flyingTime;
            vZ = (zEnd - zStart) / flyingTime;

            rb.velocity = new Vector3(vX, vY, vZ);

            onPlatform = true;
            Invoke("NotOnPlatform", 0.1f);
        }
    }

   void NotOnPlatform()
    {
        onPlatform = false;
    }
}    
