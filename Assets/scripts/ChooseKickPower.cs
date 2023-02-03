using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseKickPower : MonoBehaviour
{

    public float currentValue = 0f;

    public float minValueNormalased;
    public float maxValueNormalased;

    public float maxForceMultiplaur;
    public float minForceMultiplaur;

    public static float step = 0.05f;

    public HandsManagment TopHands;
    public GameObject TopHandsGameObject;
    //public HandsManagment BottomHands;

    public Gradient gradient;

    // Update is called once per frame
    void Update()
    {
        
        if (TopHandsGameObject.active)
        {
            currentValue += step * Input.mouseScrollDelta.y;

            if (currentValue > maxValueNormalased) currentValue = maxValueNormalased;
            else if (currentValue < minValueNormalased) currentValue = minValueNormalased;

            gameObject.GetComponent<Image>().fillAmount = currentValue;
            gameObject.GetComponent<Image>().color = gradient.Evaluate(currentValue);

            //Debug.Log(TopHandsGameObject.active);
            if (Input.mouseScrollDelta.y != 0)
            {
                TopHands.maxKickForce = minForceMultiplaur + currentValue * (maxForceMultiplaur - minForceMultiplaur);
                //BottomHands.maxKickForce = minForceMultiplaur + currentValue * (maxForceMultiplaur - minForceMultiplaur);
            }
        }
       
    }
}