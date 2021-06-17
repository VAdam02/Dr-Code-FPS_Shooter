using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeak : MonoBehaviour
{
    public ParticleSystem leak;
    public List<ParticleSystem> leaks;

    // Start is called before the first frame update
    void Start()
    {
        leaks = new List<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        jerrycan can = (jerrycan)GetComponent(typeof(jerrycan));

        for (int i = 0; i < leaks.Count; i++)
        {
            if (leaks[i].isEmitting)
            {
                // 0 - 1
                double height = leaks[i].transform.localPosition.y;
                double percent = can.fuel / 500;

                double relativepercent = (percent - height) / (1-height);

                if (relativepercent > 0.1)
                {

                    leaks[i].startSpeed = (float)relativepercent * 2;
                    leaks[i].startSize = (float)relativepercent * 0.25f;

                    can.fuel = can.fuel - (10 * Time.deltaTime * relativepercent);
                }
                else
                {
                    leaks[i].Stop();
                }

                if (can.fuel < 0)
                {
                    can.fuel = 0;
                    leaks[i].Stop();
                }
            }

            if (leaks[i].isStopped)
            {
                Destroy(leaks[i].gameObject);
                leaks.RemoveAt(i);

                i--;
            }
        }

        /*
        if (leaks.Count > 0)
        {
            Debug.Log(leaks[0].isEmitting);
        }
        */
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            for (int i = 0; i < leaks.Count; i++)
            {
                leaks[i].Stop();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
        {
            Vector3 point = new Vector3(0, 0, 0);
            foreach (ContactPoint contact in collision.contacts)
            {
                point += contact.point;
            }
            point /= collision.contacts.Length;

            ParticleSystem particle = Instantiate(leak);
            particle.transform.SetParent(this.transform.GetChild(0));
            particle.transform.position = point;
            particle.transform.LookAt(collision.gameObject.transform);
            particle.Play();

            leaks.Add(particle);
        }
    }
}
