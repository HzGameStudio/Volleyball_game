using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotBasicData : MonoBehaviour
{
    public GameObject ball;


    public Transform transform;
    public Vector3 targetPosition;

    public bool isRaning = false;

    public float travelVelocity;
    public float timeOfReaction;


    public float handsHeidht;

    public bool isTimeofReactionRaning = false;
    public float currentTimeOfReaction = 0f;


    // Update is called once per frame
    void Update()
    {
        Move();

        SetTimerOfReaction();

        CalculateTimeOfRection();

    }

    public void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        Vector3 speed = (targetPosition - transform.position).normalized * travelVelocity * Time.deltaTime;
        if(transform.position != targetPosition)
        {
            if (distance > speed.magnitude)
            {
                transform.position += speed;
                isRaning = true;
            }
            else
            {
                transform.position = targetPosition;
                isRaning = false;
            }
        }
    }

    public Vector3 GetFallPointPosotion(Transform ball, Rigidbody rb, float groundY, Vector3 fallPoint)
    {
        float fallAcseleration = 30f;
        float velocityX, velocityY, velocityZ;
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;
        velocityZ = rb.velocity.z;

        Debug.Log("bot velocyty " + rb.velocity.ToString());
        Debug.Log("bot coors" + ball.position.ToString());
        Debug.Log("Bot" + Time.timeSinceLevelLoad);

        groundY = 24.7f + 1.5f;

        float x, y, z;
        x = ball.position.x;
        y = ball.position.y - groundY;
        z = ball.position.z;

        Vector3 newFallPoint;
        float flyingTime = 0f;
        //if(y < 0)
        //{
        //    y = Mathf.Abs(y);

        //    float discriminator = velocityY * velocityY - 2 * fallAcseleration * y;


        //}
        if (velocityY > 0)
        {
            if(y>=0)
            {
                float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;
                
                if (discriminator >= 0)
                {
                    flyingTime = 2 * velocityY / fallAcseleration + (-velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;
                }
                else
                {
                    return fallPoint;
                }
            }
            else
            {
                float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;
                
                if (discriminator >= 0)
                {
                    flyingTime = (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;
                }
                else
                {
                    return fallPoint;
                }
            }
            
            //float flyingTime = 2 * velocityY / fallAcseleration + (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;

            newFallPoint.y = groundY;
            newFallPoint.x = x + flyingTime * velocityX;
            newFallPoint.z = z + flyingTime * velocityZ;
        }
        else
        {
            float discriminator = velocityY * velocityY + 2 * fallAcseleration * y;

            flyingTime = (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;

            if (discriminator >= 0)
            {
                flyingTime = 2 * velocityY / fallAcseleration + (velocityY + Mathf.Sqrt(discriminator)) / fallAcseleration;
            }
            else
            { ///sdds
                return fallPoint;
            }

            newFallPoint.y = groundY;
            newFallPoint.x = x + flyingTime * velocityX;
            newFallPoint.z = z + flyingTime * velocityZ;
        }



        //Debug.Log(fallPoint);

        if (newFallPoint.x < 0)
        {
            return fallPoint;
        }

        return newFallPoint;

    }

    private void CalculateTimeOfRection()
    {
        if(isTimeofReactionRaning)
        {
            currentTimeOfReaction += Time.deltaTime;

            if(currentTimeOfReaction>=timeOfReaction)
            {
                isTimeofReactionRaning = false;
                currentTimeOfReaction = 0f;
                //do something get target;
                targetPosition = GetFallPointPosotion(ball.transform, ball.GetComponent<Rigidbody>(), handsHeidht, targetPosition);
                Debug.Log("Change tranget");

            }
        }
    }

    private void SetTimerOfReaction()
    {
        if(!isTimeofReactionRaning)
        {
            if(ball.GetComponent<ball>().isKicked)
            {
                isTimeofReactionRaning = true;
                currentTimeOfReaction = 0f;
            }
        }
    }

    public void Teleport()
    {
        transform.position = targetPosition;
        isRaning = false;
    }

}
