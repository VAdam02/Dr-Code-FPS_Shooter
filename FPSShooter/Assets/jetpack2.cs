using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpack2 : MonoBehaviour
{
    public ParticleSystem rightfire;
    public ParticleSystem leftfire;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound.Stop();
        
    }

    float deltatime = 0;
    float soundtime = 0;
    // Update is called once per frame
    void Update()
    {
        if (soundtime > 1)
        {
            sound.time = 6;
            soundtime = 0;
            //sound.Play();
        }
        soundtime += Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            rightfire.Play();
            leftfire.Play();

            sound.time = 6;
            sound.Play();
            sound.volume = 0.25f;
            deltatime = 0;
        }
        if (!Input.GetKey("space"))
        {
            rightfire.Stop();
            leftfire.Stop();

            deltatime += Time.deltaTime;

            sound.volume = 0.25f * (0.5f - deltatime);
        }
    }
}
