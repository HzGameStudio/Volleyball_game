using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManagment : MonoBehaviour
{
    public float kickTime;

    private bool kickStarded = false;

    public float currentTime = 0f;

    private float minKickForce;
    public float maxKickForce;

    public float currentKickForce;

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
            kickStarded = true;

        }

        TimerCalculate();
    }

    private void TimerCalculate()
    {
        if (kickStarded)
        {
            currentTime += Time.deltaTime;
            if(currentTime<=kickTime)
            {
                ChangeKickForce();
            }
            else
            {
                currentKickForce = 0f;
                currentTime = 0f;
                kickStarded = false;
            }
        }
    }

    private void ChangeKickForce()
    {
        currentKickForce = maxKickForce - currentTime / kickTime * (maxKickForce - minKickForce);
    }
}
