using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IdealPointUI : MonoBehaviour
{
    public Gradient gradient;
    
    public Transform ball;
    public Transform idealPoint;

    public float minAnimatedDistance;

    private float currentDistance;

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(ball.position, idealPoint.position);
        Debug.Log(currentDistance);
        if(currentDistance<minAnimatedDistance)
        {
            gameObject.GetComponent<Image>().fillAmount = currentDistance / minAnimatedDistance;
            gameObject.GetComponent<Image>().color = gradient.Evaluate(currentDistance / minAnimatedDistance);
        }
        else
        {
            gameObject.GetComponent<Image>().fillAmount = 1f;
            gameObject.GetComponent<Image>().color = gradient.Evaluate(currentDistance / minAnimatedDistance);
        }
        
        
    }
}
