using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJetpack : MonoBehaviour
{
    public bool rocketing = false;
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
    public void StartJetpack()
    {
        rocketing = true;

        rightfire.Play();
        leftfire.Play();

        sound.time = 0;
        sound.volume = 0.25f;
        sound.Play();

        deltatime = 0;
    }

    public void StopJetpack()
    {
        rocketing = false;
        sound.time = 9.5f;
        deltatime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (deltatime > 9.5f && rocketing)
        {
            sound.time = 1;
            deltatime = 1;
        }

        if (!rocketing)
        {
            rightfire.Stop();
            leftfire.Stop();
        }
    }
}
