using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpack : MonoBehaviour
{
    public ParticleSystem rightfire;
    public ParticleSystem leftfire;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound.Stop();
        rightfire.Stop();
        leftfire.Stop();
    }

    float deltatime = 0;
    // Update is called once per frame
    void Update()
    {
        //2-9.5
        if (Input.GetKeyDown("space"))
        {
            rightfire.Play();
            leftfire.Play();

            sound.time = 0;
            sound.volume = 0.25f;
            sound.Play();

            deltatime = 0;
        }

        if (deltatime > 9.5f)
        {
            sound.time = 1;
            deltatime = 1;
        }

        if (Input.GetKeyUp("space"))
        {
            rightfire.Stop();
            leftfire.Stop();

            sound.time = 9.5f;
            deltatime = 1;
        }
    }
}
