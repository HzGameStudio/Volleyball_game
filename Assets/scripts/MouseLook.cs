using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float Sensitivity = 100f;

    public Transform playerBody;

    public GameObject BottomHandsPosition;
    public GameObject TopHandsPosition;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //if(transform.rotation.x >0)
        //{
        //    if (TopHandsPosition.active || !BottomHandsPosition.active)
        //    {
        //        TopHandsPosition.SetActive(false);
        //        BottomHandsPosition.SetActive(true);
        //    }
                
        //}
        //else
        //{
        //    if(BottomHandsPosition.active || !TopHandsPosition.active)
        //    {
        //        TopHandsPosition.SetActive(true);
        //        BottomHandsPosition.SetActive(false);
        //    }
        //}
    }
}
