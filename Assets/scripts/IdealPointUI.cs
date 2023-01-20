using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IdealPointUI : MonoBehaviour
{
    public Gradient gradient;
    
    public Transform ball;
    public Transform idealPointTopHandsPosition;
    public Transform idealPointBottomHandsPosition;
    public ChangeHandsPosition script;

    public float minAnimatedDistance;

    private float currentDistance;

    // Update is called once per frame
    void Update()
    {
        if(script.positionFlag)
        {
            currentDistance = Vector3.Distance(ball.position, idealPointBottomHandsPosition.position);
        }
        else
        {
            currentDistance = Vector3.Distance(ball.position, idealPointTopHandsPosition.position);
        }
        
        //Debug.Log(currentDistance);
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
