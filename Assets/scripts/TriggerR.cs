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
        anim.SetTrigger("Running");
        Invoke("stopRun", 0.1f);
    }

    public void TriggerWait()
    {
        anim.SetTrigger("Waiting");
        Invoke("stopWait", 0.1f);
    }

    public void TriggerAnimeKick()
    {
        anim.SetTrigger("Kick");
        Invoke("stopAnimeKick", 0.1f);
    }

    void stopRun()
    {
        anim.ResetTrigger("Running");
    }
    void stopWait()
    {
        anim.ResetTrigger("Waiting");
    }
    void stopAnimeKick()
    {
        anim.ResetTrigger("Kick");
    }
   
}
