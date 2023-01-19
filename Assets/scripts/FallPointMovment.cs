using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPointMovment : MonoBehaviour
{
    public Transform fallPoint;

    public Rigidbody rb;

    /*void Update()
    {
        if(rb.velocity.y>0)
        {
            fallPoint.position = GetFallPointPosotion();
        }
    }*/

    private Vector3 GetFallPointPosotion()
    {
        float fallAcseleration = 30f;
        float velocityX, velocityY, velocityZ;
        velocityX= rb.velocity.x;
        velocityY= rb.velocity.y;
        velocityZ= rb.velocity.z;

        float groundY = 21.5f;

        float x, y,z;
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y - groundY;
        z = gameObject.transform.position.z;

        float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;

        float flyingTime = 2 * velocityY / fallAcseleration + (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;

        Vector3 fallPoint;

        fallPoint.y = groundY;
        fallPoint.x = x + flyingTime * velocityX;
        fallPoint.z = z + flyingTime* velocityZ;

        //Debug.Log(fallPoint);

        return fallPoint;

    }
}
