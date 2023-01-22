using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPlaing : MonoBehaviour
{
    // Start is called before the first frame update
    private BotBasicData bot;

    public float botVelocity;
    
    void Update()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;       
    }
}
