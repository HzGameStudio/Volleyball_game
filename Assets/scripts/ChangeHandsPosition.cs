using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChangeHandsPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TopHandsPosition;
    public GameObject TopHandsAnimetionTools;

    public GameObject BottomHandsPosition;

    public static bool positionFlag = false;// flase TopPosition, true BottomPosition

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BottomHandsPosition.SetActive(false);
            TopHandsPosition.SetActive(true);
            TopHandsAnimetionTools.SetActive(true);
            positionFlag = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            BottomHandsPosition.SetActive(true);
            TopHandsPosition.SetActive(false);
            TopHandsAnimetionTools.SetActive(false);
            positionFlag = true;
        }
    }

    //to fix my beloved animations
    /*private void Awake()
    {
        BottomHandsPosition.SetActive(false);
        TopHandsPosition.SetActive(true);
        TopHandsAnimetionTools.SetActive(true);
        positionFlag = false;
    }*/
}
