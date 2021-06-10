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
        for (int i = 0; i < leaks.Count; i++)
        {
            if (leaks[i].isStopped)
            {
                Destroy(leaks[i].gameObject);
                leaks.RemoveAt(i);

                i--;
            }
        }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("bullet"))
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

            Destroy(collision.gameObject);
        }
    }
}
