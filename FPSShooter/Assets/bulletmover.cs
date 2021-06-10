using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * Time.deltaTime * 5;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}