using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider mouseSlider;
    public TextMeshProUGUI senseUI;
    public Slider scrollSlider;
    public TextMeshProUGUI sense1UI;

    float mSense = 100f;
    float sSense = 2f;

    private void Start()
    {
        mouseSlider.value = mSense;
        scrollSlider.value = sSense;
    }

    public void SetMouseSens(float sens)
    {
        MouseLook.Sensitivity = sens;
        mSense = sens;
        senseUI.text = sens.ToString("0");
    }

    public void SetScrollSens(float sens)
    {
        ChooseKickPower.step = sens / 40f;
        ChangeBottomPosition.step = sens;
        sSense = sens;
        sense1UI.text = sens.ToString("0");
    }
}
