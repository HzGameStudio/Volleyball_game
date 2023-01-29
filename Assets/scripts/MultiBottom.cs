using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiBottom : MonoBehaviour
{
    public float kickTime;

    //public Transform idealKickPoint;
    //public Transform ball;

    private bool kickStarded = false;
    private bool refilingKick = false;

    public float currentTime = 0f;

    private float minKickForce;
    public float maxKickForce;

    private float realMaxKickForce;
    private float realMinKickForce;

    public float currentKickForce;

    public GameObject LeftArm;
    public GameObject RightArm;

    public GameObject player;
    PhotonView view;

    private void Start()
    {
        minKickForce = maxKickForce * 0.8f;
        view = player.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!refilingKick)
                {
                    kickStarded = true;
                    LeftArm.GetComponent<TriggerR>().TriggerKick();
                    RightArm.GetComponent<TriggerR>().TriggerKick();

                    currentKickForce = 1800f;
                    Invoke("ForceZero", 0.3f);
                }


            }
        }
        
    }

    void ForceZero()
    {
        currentKickForce = 0f;
    }

    
}
