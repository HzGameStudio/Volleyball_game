using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPointMovment : MonoBehaviour
{
    public Transform fallPoint;

    public Rigidbody rb;

    private bool flag = true;

    void Update()
    {
        if(gameObject.transform.position.x > 0 && flag)
        {
            
            fallPoint.position = GetFallPointPosotion();
            flag = false;
        }
        else if(gameObject.transform.position.x<0 && rb.velocity.x<0)
        {
            flag = true;
        }
    }

    private Vector3 GetFallPointPosotion()
    {
        float fallAcseleration = 30f;
        float velocityX, velocityY, velocityZ;
        velocityX= rb.velocity.x;
        velocityY= rb.velocity.y;
        velocityZ= rb.velocity.z;

        float groundY = 24.7f + 1.5f;

        float x, y,z;
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y - groundY;
        z = gameObject.transform.position.z;

        Vector3 fallPoint;

        if (velocityY>0)
        {
            float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;

            float flyingTime = 2 * velocityY / fallAcseleration + (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;

            fallPoint.y = groundY;
            fallPoint.x = x + flyingTime * velocityX;
            fallPoint.z = z + flyingTime * velocityZ;
        }
        else
        {
            float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;

            float flyingTime = (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;

            fallPoint.y = groundY;
            fallPoint.x = x + flyingTime * velocityX;
            fallPoint.z = z + flyingTime * velocityZ;
        }

        

        //Debug.Log(fallPoint);

        return fallPoint;

    }
}
