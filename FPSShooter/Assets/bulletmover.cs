using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmover : MonoBehaviour
{
    float distant = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 deltadistant = gameObject.transform.forward * Time.deltaTime * 10;
        gameObject.transform.position += deltadistant;
        distant += Vector3.Distance(new Vector3(0, 0, 0), deltadistant);

        if (distant > 100)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}