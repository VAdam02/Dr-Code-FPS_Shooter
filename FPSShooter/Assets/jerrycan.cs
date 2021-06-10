using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jerrycan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("igen");
        jetpack jetpack = (jetpack) collision.gameObject.GetComponent(typeof(jetpack));
        jetpack.fuel = jetpack.fuel + 500;
        if (jetpack.fuel > 1000)
        {
            jetpack.fuel = 1000;
        }
        
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
