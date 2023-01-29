using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    PhotonView view;

    private void Start()
    {
        view = player.GetComponent<PhotonView>();
        if (!view.IsMine)
        {
            cam.enabled = false;
        }
    }
}
