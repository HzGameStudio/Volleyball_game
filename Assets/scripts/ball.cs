using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;
using UnityEditorInternal;
using System.Runtime.CompilerServices;

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

    float xMin = -30f;
    float xMax = -20f;
    float zMin = -10f;
    float zMax = 10f;
    float maxHeight = 40f;

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

    public GameObject unityChan;

    private void OnCollisionEnter(Collision collision)
    {
        //isKicked = false;
        if (collision.gameObject.CompareTag("Wall") && !onPlatform)
        {
            if(!Bot.GetComponent<BotBasicData>().isRaning)
            {
                xStart = transform.position.x;
                zStart = transform.position.z;

                // if bot is ready too aim his shot
                if (Bot.GetComponent<BotBasicData>().ReadyTime > 0)
                { 
                    float[] corners = { xMin, zMin, xMin, zMax, xMax, zMin, xMax, zMax };
                    int furthest_corner = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (Vector2.Distance(new Vector2(bodyAngle.position.x, bodyAngle.position.z), new Vector2(corners[i * 2], corners[i * 2 + 1])) > Vector2.Distance(new Vector2(bodyAngle.position.x, bodyAngle.position.z), new Vector2(corners[furthest_corner * 2], corners[furthest_corner * 2 + 1])))
                            furthest_corner = i;
                    }
                    float aim_percentage = Bot.GetComponent<BotBasicData>().ReadyTime / Bot.GetComponent<BotBasicData>().AimTimeNeed;
                    aim_percentage = Mathf.Min(aim_percentage, Bot.GetComponent<BotBasicData>().MaxAimAccuracy);
                    Vector3 aim_center = bodyAngle.position + (new Vector3(corners[furthest_corner * 2], 0.0f, corners[furthest_corner * 2 + 1]) - bodyAngle.position) * aim_percentage;
                    float aim_radius = Mathf.Min(Mathf.Abs(aim_center.x - xMin), Mathf.Abs(aim_center.x - xMax), Mathf.Abs(aim_center.z - zMin), Mathf.Abs(aim_center.z - zMax));

                    // https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly/50746409#50746409
                    float r = aim_radius * Mathf.Sqrt(Random.value);
                    float theta = Random.value * 2 * Mathf.PI;
                    xEnd = aim_center.x + r * Mathf.Cos(theta);
                    zEnd = aim_center.z + r * Mathf.Sin(theta);
                }
                // if bot is not ready to aim his shot
                else
                {
                    xEnd = Random.Range(xMin, xMax);
                    zEnd = Random.Range(zMin, zMax);
                }

                rb.velocity = GetFlySpeed(new Vector2(xStart, zStart), new Vector2(xEnd, zEnd), 10f);
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
                Bot.GetComponent<BotBasicData>().ReadyTime = 0.0f;
                onPlatform = true;
                Invoke("NotOnPlatform", 0.1f);

                unityChan.GetComponent<TriggerR>().TriggerSpKick();
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
            Bot.GetComponent<BotBasicData>().ReadyTime = 0.0f;
            onPlatform = true;
            Invoke("NotOnPlatform", 0.1f);

            unityChan.GetComponent<TriggerR>().TriggerSpKick();
        }
    }

   void NotOnPlatform()
    {
        onPlatform = false;
    }

    private Vector2 findIntersetion(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float a1 = (p1.y - p2.y) / (p1.x - p2.x);
        float b1 = p1.y - a1 * p1.x;
        float a2 = (p3.y - p4.y) / (p3.x - p4.x);
        float b2 = p3.y - a2 * p3.x;

        Vector2 intrsPoint;
        //Debug.Log(a2);
        //Debug.Log(b2);


        intrsPoint.x = (b1 - b2) / (a2 - a1);
        intrsPoint.y = a1 * intrsPoint.x + b1;

        return intrsPoint;
    }

    private Vector3 GetFlySpeed(Vector2 startPoint, Vector2 finishPoint, float heigth)
    {
        Vector3 velocity;
        Vector2 netPoint = findIntersetion(new Vector2(10, 0), new Vector2(20, 0), startPoint, finishPoint);
        float c = netPoint.x;
        netPoint.x = netPoint.y;
        netPoint.y = c;

        float k1 = Mathf.Sqrt((netPoint.x - startPoint.x) * (netPoint.x - startPoint.x) + (netPoint.y - startPoint.y) * (netPoint.y - startPoint.y));
        //Debug.Log(netPoint);
        float k2 = Mathf.Sqrt((finishPoint.x - startPoint.x) * (finishPoint.x - startPoint.x) + (finishPoint.y - startPoint.y) * (finishPoint.y - startPoint.y));

        velocity.y = Mathf.Sqrt(heigth * gravity / (2 * (k2 - k1) * k1)) * k2;

        float velocityK = Mathf.Sqrt(gravity * (k2 - k1) * k1 / (2 * heigth));

        velocity.x = velocityK * (finishPoint.x - startPoint.x) / k2;
        velocity.z = velocityK * (finishPoint.y - startPoint.y) / k2;

        return velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("animtr"))
        {
            unityChan.GetComponent<TriggerR>().TriggerSpKick();
        }
    }
}    
