using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KickEfectUI : MonoBehaviour
{
    // Update is called once per frame

    public HandsManagment topHandsManager;
    public HandsManagment bottomHandsManager;

    void Update()
    {
        if(topHandsManager.isActiveAndEnabled)
        {
            gameObject.GetComponent<Image>().fillAmount = 1f - topHandsManager.currentTime / topHandsManager.kickTime;
        }
        else
        {
            gameObject.GetComponent<Image>().fillAmount = 1f - bottomHandsManager.currentTime / bottomHandsManager.kickTime;
        }
        
        //if(gameObject.GetComponent<Image>().fillAmount == 0)
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
