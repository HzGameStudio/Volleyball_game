using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBottomPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bottomHandPosition;
    public static float step = 2f;

    public float minAngle;
    public float maxAngle;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.active)
        {
            Vector3 rotation = bottomHandPosition.localRotation.eulerAngles;

            //Debug.Log(rotation);
            rotation.x += step * Input.mouseScrollDelta.y;
            //rotation.x = Mathf.Clamp(rotation.x, -70f, 70f);

            if(rotation.x<=90)
            {
                if(rotation.x>=minAngle)
                {
                    rotation.x = minAngle;
                }
            }
            else if(rotation.x >=270)
            {
                if(rotation.x<maxAngle)
                {
                    rotation.x = maxAngle;
                }
            }
            bottomHandPosition.localRotation = Quaternion.Euler(rotation);
        }
    }
}
