using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_script : MonoBehaviour
{
    private float duration_time;
    public float interval_time;
    private bool is_started = false;

    public float cur_time_left;
    private float cur_int_time;

    public Transform Time_sprite;
    public GameObject Sprite_manager;

    //public GameObject On_object;

    public void Set_timer(float time)
    {
        cur_time_left = time;
        duration_time = time;
        is_started  = true;
        cur_int_time = interval_time;
        Sprite_manager.SetActive(true);
    }

    //private void Start()
    //{
    //    Set_timer();
    //}
    // Update is called once per frame
    void Update()
    {
        if(is_started)
        {
            if (cur_time_left > 0)
            {
                if (cur_int_time <= 0)
                {
                    cur_time_left -= interval_time;
                    Show_Cur_Time();
                    cur_int_time = interval_time;
                }
                else cur_int_time -= Time.deltaTime;
            }
            else End_Timer();
        }
    }

    public void End_Timer()
    {
        is_started = false;
        //Destroy(gameObject);
        //On_object.GetComponent<Tank_bust_script>().End_busts();
        Sprite_manager.SetActive(false);
    }

    public void Show_Cur_Time()
    {
        float time_percent = cur_time_left / duration_time;
        Time_sprite.localScale = new Vector3(time_percent * 2.8f, 0.8f, 0f);
        //Health_bar.position = Vector3.zero;
        Time_sprite.localPosition = new Vector3(-1.4f * (1 - time_percent), 0f, 0f);
    }
}
