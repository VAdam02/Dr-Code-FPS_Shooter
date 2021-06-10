using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private static TcpClient client;
    private static NetworkStream stream;

    // Start is called before the first frame update
    void Start()
    {
        client = new TcpClient("127.0.0.1", 50000);
        stream = client.GetStream();
    }

    // Update is called once per frame
    void Update()
    {
        stream.Write(new byte[] { 0, 1, 2, 3, 4, 5, 6 }, 0, 7);
    }
}
