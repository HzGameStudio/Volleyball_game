using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnballslol : MonoBehaviour
{

    public GameObject ball;
    public Rigidbody rb;

    private void Update()
    {
        if (ball.transform.position.y < 0)
        {
            ball.transform.position = new Vector3(-15, 34, 9);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public void awn()
    {
        Instantiate(ball, transform.position, transform.rotation);
    }
}
