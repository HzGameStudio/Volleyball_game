using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ball : MonoBehaviour
{
    public Rigidbody rb;
    public float kickForce = 10f;
    public Transform cameraAngle;
    public Transform bodyAngle;

    public bool isKicked = false;

    public TextMeshProUGUI BotScore;
    float BotPoints = 0;
    public TextMeshProUGUI PlayerScore;
    float PlayerPoints = 0;

    public float Ballgravity;


    //public FallPointMovment fallPointMovmentScript;

    Vector3 appliedForce;


    //private void Update()
    //{
    //    Vector3 speed = rb.velocity;
    //    speed.y += Ballgravity * Time.deltaTime;

    //    rb.velocity = speed;
    //}
    void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("topHands"))
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

                //fallPointMovmentScript.fallPoint.position = fallPointMovmentScript.GetFallPointPosotion();
            }
           
        }
        else if(collision.CompareTag("bottomHands"))
        {
            kickForce = collision.gameObject.GetComponent<HandsManagerBottomPosition>().currentKickForce;
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
            //fallPointMovmentScript.fallPoint.position = fallPointMovmentScript.GetFallPointPosotion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isKicked = false;
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

    public GameObject Bot;

    private void OnCollisionEnter(Collision collision)
    {
        //isKicked = false;
        if (collision.gameObject.CompareTag("Wall") && !onPlatform)
        {
            if(!Bot.GetComponent<BotBasicData>().isRaning)
            {
                xStart = transform.position.x;
                zStart = transform.position.z;
                xEnd = Random.Range(xMin, xMax);
                zEnd = Random.Range(yMin, yMax);

                flyingTime = Mathf.Sqrt(2 * maxHeight / gravity) * 2f;

                vY = gravity * flyingTime / 2;
                vX = (xEnd - xStart) / flyingTime;
                vZ = (zEnd - zStart) / flyingTime;

                rb.velocity = new Vector3(vX, vY, vZ);

                onPlatform = true;
                Invoke("NotOnPlatform", 0.1f);
            }
            else
            {
                PlayerPoints += 1;
                PlayerScore.text = (PlayerPoints/1).ToString();
                rb.velocity = new Vector3(0, 0, 0);
                transform.position = new Vector3(21, 35, 0);

                Bot.GetComponent<BotBasicData>().targetPosition = new Vector3(21, 26.2f, 0);
                Bot.GetComponent<BotBasicData>().Teleport();
                onPlatform = true;
                Invoke("NotOnPlatform", 0.1f);
            }
           
        }

        if (collision.gameObject.CompareTag("field") && !onPlatform)
        {
            BotPoints += 1;
            BotScore.text = (BotPoints/1).ToString();
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(21, 35, 0);
            Bot.GetComponent<BotBasicData>().targetPosition = new Vector3(21, 26.2f, 0);
            Bot.GetComponent<BotBasicData>().Teleport();
            onPlatform = true;
            Invoke("NotOnPlatform", 0.1f);
        }
    }

   void NotOnPlatform()
    {
        onPlatform = false;
    }
}    
