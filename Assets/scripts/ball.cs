using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public Rigidbody rb;
    public float kickForce = 10f;
    public Transform cameraAngle;
    public Transform bodyAngle;

    Vector3 appliedForce;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            appliedForce = new Vector3(kickForce * (1 - Mathf.Abs(cameraAngle.rotation.eulerAngles.x / 90)) * (1 - Mathf.Abs(bodyAngle.rotation.eulerAngles.y - 90) / 90), kickForce * cameraAngle.rotation.eulerAngles.x / 90 * -1f + 10f, kickForce * ((bodyAngle.rotation.eulerAngles.y - 90) / 90));
            rb.AddForce(appliedForce * -1f);
        }

        if (collision.CompareTag("Wall"))
        {
            appliedForce = new Vector3(-400f - transform.position.x * 8, 400f, 0f);
            rb.AddForce(appliedForce);
        }
    }

}    
