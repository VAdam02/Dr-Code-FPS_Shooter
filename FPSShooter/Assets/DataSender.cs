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
        Connection.WriteData(body.transform.position, new Vector3(head.transform.rotation.x, body.transform.rotation.y, head.transform.rotation.z));
    }
}