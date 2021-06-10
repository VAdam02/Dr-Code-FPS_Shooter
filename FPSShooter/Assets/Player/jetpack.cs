using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jetpack : MonoBehaviour
{
    public ParticleSystem rightfire;
    public ParticleSystem leftfire;
    public AudioSource sound;
    public double fuel;

    Rigidbody rb;
    public bool rocketing;

    // Start is called before the first frame update
    void Start()
    {
        rb = (Rigidbody)gameObject.transform.parent.GetComponent(typeof(Rigidbody));
        sound.Stop();
        rightfire.Stop();
        leftfire.Stop();

        rocketing = false;
    }

    float deltatime = 0;
    // Update is called once per frame
    void Update()
    {
        if (rocketing)
        {
            rb.AddForce(0, 1000 * Time.deltaTime, 0);
            fuel = fuel - (1000 / 20 * Time.deltaTime);
        }

        //2-9.5
        if (Input.GetKeyDown("space") && fuel > 0)
        {
            rocketing = true;
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

        if (Input.GetKeyUp("space") || fuel < 0 )
        {
            rocketing = false;
            rightfire.Stop();
            leftfire.Stop();

            sound.time = 9.5f;
            deltatime = 1;
        }
    }
}
