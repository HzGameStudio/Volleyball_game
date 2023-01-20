using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerR : MonoBehaviour
{
    
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    
    public void TriggerKick()
    {
        anim.SetTrigger("Active");
    }
}
