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


    public void TriggerRun()
    {
        anim.SetTrigger("Run");
        Invoke("runStop", 0.05f);
    }

    public void TriggerWait()
    {
        anim.SetTrigger("Wait");
        Invoke("waitStop", 0.05f);
    }

    public void TriggerSpKick()
    {
        anim.SetTrigger("Kick");
        Invoke("kickStop", 0.05f);
    }
    void runStop()
    {
        anim.ResetTrigger("Run");
    }
    void waitStop()
    {
        anim.ResetTrigger("Wait");
    }
    void kickStop()
    {
        anim.ResetTrigger("Kick");
    }

   
}
