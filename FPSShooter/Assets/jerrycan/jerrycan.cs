using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jerrycan : MonoBehaviour
{
    public double deltatime = 0;
    GameObject can;

    // Start is called before the first frame update
    void Start()
    {
        can = gameObject.transform.GetChild(0).gameObject;
        gameObject.SetActive(true);
    }

    float position = 0;
    // Update is called once per frame
    void Update()
    {
        if (deltatime > 10)
        {
            can.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            deltatime += Time.deltaTime;
        }

        can.transform.localEulerAngles = new Vector3(can.transform.eulerAngles.x, can.transform.eulerAngles.y + (360 / 6 * Time.deltaTime), can.transform.eulerAngles.z);

        position += Time.deltaTime / 2;
        // 0 - 1
        if (position < 1)
        {
            can.transform.localPosition = new Vector3(can.transform.localPosition.x, position, can.transform.localPosition.z);
        }
        // 1 - 2
        else if (position < 2)
        {
            can.transform.localPosition = new Vector3(can.transform.localPosition.x, 2 - position, can.transform.localPosition.z);
        }
        //reset
        else
        {
            position -= 2;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("player"))
        {
            return;
        }

        GameObject jetpackObject = collision.gameObject.transform.GetChild(2).gameObject;

        jetpack jetpack = (jetpack)jetpackObject.GetComponent(typeof(jetpack));
        jetpack.fuel = jetpack.fuel + 500;
        if (jetpack.fuel > 1000)
        {
            jetpack.fuel = 1000;
        }

        deltatime = 0;
        can.transform.GetChild(0).gameObject.SetActive(false);
    }
}
