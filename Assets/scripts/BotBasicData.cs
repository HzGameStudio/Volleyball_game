using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class BotBasicData : MonoBehaviour
{
    public GameObject ball;


    public Transform BotTransform;
    public Vector3 targetPosition;

    public bool isRaning = false;

    public float travelVelocity;
    public float timeOfReaction;

    public float handsHeidht;

    public bool isTimeofReactionRaning = false;
    public float currentTimeOfReaction = 0f;

    // if ReadyTime is negative, the bot is not ready to kick
    public float ReadyTimeNeed;
    public float AimTimeNeed;
    public float MaxAimAccuracy;
    public float ReadyTime = 0.0f;

    public GameObject unityChan;
    Vector2 catheuses;

    public GameObject player;

    float chanAngle;
    bool rotaionNormalized = true;
    bool rotated = true;

    public Vector3 starting_position;

    public bool IsTestMode = false;

    private void Start()
    {
        targetPosition = BotTransform.position;
        starting_position = BotTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        SetTimerOfReaction();

        CalculateTimeOfRection();

        CalculateReadyTime();

        //rotates unity chan so she faces fall point (get rotated, unity chan! UwU)
        if (isRaning)
        {
            unityChan.GetComponent<TriggerR>().TriggerRun();
            if (!rotated)
            {
                //unity rotation system is weird so I fix it here
                if (catheuses.y < 0)
                {
                    catheuses.y = Mathf.Abs(catheuses.y);
                    chanAngle = Mathf.Atan(catheuses.x / catheuses.y) * Mathf.Rad2Deg;
                    catheuses.y = -catheuses.y;
                }
                else
                {
                    chanAngle = 180 - Mathf.Atan(catheuses.x / catheuses.y) * Mathf.Rad2Deg;
                }
                chanAngle = chanAngle - 90f;

                unityChan.transform.Rotate(0f, chanAngle, 0f);
                rotated = true;
                rotaionNormalized = false;
            }
        }
        else
        {   //normalizes rotarion of inty chan so she faces opposite side of the field
            unityChan.GetComponent<TriggerR>().TriggerWait();
            if (!rotaionNormalized)
            {
                unityChan.transform.Rotate(0f, -chanAngle, 0f);
                rotaionNormalized = true;
            }
        }
    }

    public void Move()
    {
        float distance = Vector3.Distance(BotTransform.position, targetPosition);

        Vector3 speed = (targetPosition - BotTransform.position).normalized * travelVelocity * Time.deltaTime;
        if(BotTransform.position != targetPosition)
        {
            if (distance > speed.magnitude)
            {
                BotTransform.position += speed;
                isRaning = true;
            }
            else
            {
                BotTransform.position = targetPosition;
                isRaning = false;
            }
        }
    }

    public GameObject deleteLater;

    public Vector3 GetFallPointPosotion(Transform ball, Rigidbody rb, float groundY, Vector3 fallPoint)
    {
        float fallAcseleration = 30f;
        float velocityX, velocityY, velocityZ;
        velocityX = rb.velocity.x;
        velocityY = rb.velocity.y;
        velocityZ = rb.velocity.z;

        //Debug.Log("bot velocyty " + rb.velocity.ToString());
        //Debug.Log("bot coors" + ball.position.ToString());
        //Debug.Log("Bot" + Time.timeSinceLevelLoad);

        groundY = 24.7f + 1.5f;

        float x, y, z;
        x = ball.position.x;
        y = ball.position.y - groundY;
        z = ball.position.z;

        Vector3 newFallPoint;
        float flyingTime = 0f;

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



        if (newFallPoint.x < 0)
        {
            return fallPoint;
        }

        catheuses = new Vector2 ((-newFallPoint.x + unityChan.transform.position.x), (newFallPoint.z - unityChan.transform.position.z));
        deleteLater.transform.position = newFallPoint;
        rotated = false;

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
                //Debug.Log("Change tranget");

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
        BotTransform.position = targetPosition;
        isRaning = false;
    }

    private void CalculateReadyTime()
    {
        if (BotTransform.position == targetPosition)
            ReadyTime += Time.deltaTime;
        else
            ReadyTime = 0.0f;
    }

    public void ChangeDifficulty(string diff)
    {
        if (diff == "Test")
        {
            Debug.Log("Test Mode Active");
            IsTestMode = true;
            travelVelocity = 20f;
            player.gameObject.GetComponent<PlayerMovement>().normalSpeed = 18f;
            return;
        }
        else
            IsTestMode = false;
        if (diff == "Hard")
        {
            Debug.Log("Hard Mode Active");
            ReadyTimeNeed = 0.3f;
            AimTimeNeed = 1.1f;
            MaxAimAccuracy = 0.9f;
            travelVelocity = 20f;
            player.gameObject.GetComponent<PlayerMovement>().normalSpeed = 15f;
            return;
        }
        if (diff == "Medium")
        {
            Debug.Log("Medium Mode Active");
            ReadyTimeNeed = 0.5f;
            AimTimeNeed = 1.6f;
            MaxAimAccuracy = 0.8f;
            travelVelocity = 20f;
            player.gameObject.GetComponent<PlayerMovement>().normalSpeed = 18f;
            return;
        }
        if (diff == "Easy")
        {
            Debug.Log("Easy Mode Active");
            ReadyTimeNeed = 0.5f;
            AimTimeNeed = 2.0f;
            MaxAimAccuracy = 0.7f;
            travelVelocity = 15f;
            player.gameObject.GetComponent<PlayerMovement>().normalSpeed = 18f;
            return;
        }
    }
}
