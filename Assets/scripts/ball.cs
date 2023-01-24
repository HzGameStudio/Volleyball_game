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

    float xMin = -40f;
    float xMax = -15f;
    float zMin = -30f;
    float zMax = 30f;
    float maxHeight = 30f;

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

                Debug.Log(Bot.GetComponent<BotBasicData>().ReadyTime);
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

                    float r = aim_radius * Mathf.Sqrt(Random.value);
                    float theta = Random.value * 2 * Mathf.PI;
                    xEnd = aim_center.x + r * Mathf.Cos(theta);
                    zEnd = aim_center.z + r * Mathf.Sin(theta);
                    Debug.Log("Bot Ready");
                    Debug.Log(aim_percentage);
                    Debug.Log(aim_center);
                    Debug.Log(aim_radius);
                }
                else
                {
                    xEnd = Random.Range(xMin, xMax);
                    zEnd = Random.Range(zMin, zMax);
                    Debug.Log("Bot Not Ready");
                }

                Debug.Log(xEnd);
                Debug.Log(zEnd);

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
                Bot.GetComponent<BotBasicData>().ReadyTime = 0.0f;
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
            Bot.GetComponent<BotBasicData>().ReadyTime = 0.0f;
            onPlatform = true;
            Invoke("NotOnPlatform", 0.1f);
        }
    }

   void NotOnPlatform()
    {
        onPlatform = false;
    }
}    
