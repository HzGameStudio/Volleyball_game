using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManagerBottomPosition : MonoBehaviour
{
    public float kickTime;

    public Transform idealKickPoint;
    public Transform ball;

    private bool kickStarded = false;
    private bool refilingKick = false;

    public float currentTime = 0f;

    private float minKickForce;
    public float maxKickForce;

    private float realMaxKickForce;
    private float realMinKickForce;

    public float currentKickForce;

    //public GameObject LeftArm;
    //public GameObject RightArm;

    private void Start()
    {
        currentKickForce = 0;
        minKickForce = maxKickForce * 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!refilingKick)
            {
                kickStarded = true;
                GetRealKickForce();
                //LeftArm.GetComponent<TriggerR>().TriggerKick();
                //RightArm.GetComponent<TriggerR>().TriggerKick();
            }
           

        }

        TimerCalculate();
    }

    private void TimerCalculate()
    {
        if (kickStarded && !refilingKick)
        {
            currentTime += Time.deltaTime;
            if(currentTime<=kickTime)
            {
                ChangeKickForce();
            }
            else
            {
                currentKickForce = 0f;
                kickStarded = false;
                refilingKick = true;
            }
        }
        else if(refilingKick)
        {
            if(currentTime>0)
            {
                currentTime -= Time.deltaTime / 1.3f;

            }else
            {
                currentTime = 0f;
                refilingKick = false;
            }
        }
    }

    private void ChangeKickForce()
    {
        currentKickForce = realMaxKickForce - currentTime / kickTime * (realMaxKickForce - realMinKickForce);
    }

    private void GetRealKickForce()
    {
        float distance = Vector3.Distance(ball.position, idealKickPoint.position);

        realMaxKickForce = maxKickForce / (5 + distance)*5;
        realMinKickForce = realMaxKickForce * 0.95f;
    }
}
