using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiChange : MonoBehaviour
{
    public GameObject TopHandsPosition;
    public GameObject BottomHandsPosition;

    public GameObject player;
    PhotonView view;

    public static bool positionFlag = false;// flase TopPosition, true BottomPosition

    private void Start()
    {
        view = player.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BottomHandsPosition.SetActive(false);
                TopHandsPosition.SetActive(true);
                positionFlag = false;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                BottomHandsPosition.SetActive(true);
                TopHandsPosition.SetActive(false);
                positionFlag = true;
            }
        }

        
    }
}
