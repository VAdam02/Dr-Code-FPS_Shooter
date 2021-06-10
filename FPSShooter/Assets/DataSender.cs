using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSender : MonoBehaviour
{
    GameObject head;
    GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        head = gameObject;
        body = head.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Connection.WriteData(0, body.transform.position, new Vector3(head.transform.eulerAngles.x/3, body.transform.eulerAngles.y/3, head.transform.eulerAngles.z/3));
    }
}