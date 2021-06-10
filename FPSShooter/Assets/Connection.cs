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

    }

    public static void WriteData(byte[] data)
    {
        stream.Write(data, 0, data.Length);
    }
    public static void WriteData(Vector3 pos, Vector3 rot)
    {
        byte[] data = new byte[7];


    }

    public static byte DecToBin(int num)
    {
        int[] binary = new int[8];

        double cache = num;
        // +/-
        if (cache < 0)
        {
            binary[0] = 0;
            cache *= -1;
        }
        else
        {
            binary[0] = 1;
        }

        //dec to bin
        int i = 7;
        while (i > 0)
        {
            binary[i] = (int)(cache % 2);
            cache = (int)(cache / 2);
            i--;
        }

        Debug.Log(binary[0] + " " + binary[1] + " " + binary[2] + " " + binary[3] + " " + binary[4] + " " + binary[5] + " " + binary[6] + " " + binary[7]);

        //bin to byte
        byte data;
        cache = 0;
        i = 0;
        while (i < 8)
        {
            cache *= 2;
            cache += binary[i];
            i++;
        }
        data = (byte)cache;
        return data;
    }
}
