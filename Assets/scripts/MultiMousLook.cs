using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiMousLook : MonoBehaviour
{
    public static float Sensitivity = 100f;

    public Transform playerBody;

    public GameObject BottomHandsPosition;
    public GameObject TopHandsPosition;

    float xRotation = 0f;

    PhotonView view;
    public GameObject player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        view = player.GetComponent<PhotonView>();
    }

    void Update()
    {

        if (view.IsMine)
        {
            float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}