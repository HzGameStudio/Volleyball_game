using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement script;

    public Image image;

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = script.currentSprintTime / script.sprintTime;
    }
}
