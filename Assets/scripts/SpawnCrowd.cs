using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrowd : MonoBehaviour
{
    public float Xmax;
    public float Xmin;
    public float Zmax;
    public float Zmin;

    public int people_in_crowd;
    public float person_scale;

    public GameObject[] People = new GameObject[5];
    public string[] animations = new string[6];
    public AnimationClip[] clips = new AnimationClip[6];

    private void Start()
    {
        for (int i = 0; i <= people_in_crowd; i++)
        {
            //get random cords
            float x = Random.Range(Xmin, Xmax);
            float z = Random.Range(Zmin, Zmax);

            //check if person is not on field
            if ( !((x < 58) && (x > -61) && (z < 55) && (z > -60)) )
            {
                //get random person
                int skin_number = (int)Random.Range(0, 4);

                //spawning person
                GameObject person;
                Vector3 cords = new Vector3(x, 20.5f, z);
                person = Instantiate(People[skin_number], cords, Quaternion.identity);

                //scaling person;
                person.transform.localScale = new Vector3(person_scale, person_scale, person_scale);

                //rotating person
                person.transform.LookAt(Vector3.zero);
                person.transform.rotation = Quaternion.Euler(0f, person.transform.eulerAngles.y, person.transform.eulerAngles.z);

                //adding animations
                for (int j = 0; j <= 5; j++)
                {
                    person.GetComponent<Animation>().AddClip(clips[j], animations[j]);
                }

                //randomizing animation
                int animation_number = (int)Random.Range(0, 5);
                person.GetComponent<Animation>().Blend(animations[animation_number], 1f, 0.1f);
            }
        }
    }
}
