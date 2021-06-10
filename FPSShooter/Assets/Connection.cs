using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Connection : MonoBehaviour
{
    private static TcpClient client;
    private static NetworkStream stream;
    public GameObject enemy; //amit másolni fog az ellenséges játékosokhoz

    public static DataReceiver[] enemys = new DataReceiver[255];
    public static bool[] spawned = new bool[enemys.Length];

    public static Thread reader;
    static bool reading;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawned.Length; i++) { spawned[i] = false; }

        client = new TcpClient("127.0.0.1", 50000);
        stream = client.GetStream();

        reader = new Thread(() => ReadData(enemy));
        reader.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ReadData(GameObject enemy)
    {
        reading = true;
        while (reading)
        {
            byte[] data = new byte[13];
            stream.Read(data, 0, data.Length);

            if (!spawned[data[0]])
            {
                GameObject xy = Instantiate(enemy);
                spawned[data[0]] = true;
                enemys[data[0]] = xy.GetComponent<DataReceiver>();
            }
            enemys[data[0]].ReadData(data);
        }
    }

    public static void WriteData(byte[] data)
    {
        stream.Write(data, 0, data.Length);
    }
    public static void WriteData(Vector3 pos, Vector3 rot)
    {
        byte[] data = new byte[13];

        data[0] = 0;

        //pos x
        byte[] cache = DecToBin(pos.x);
        data[1] = cache[0];
        data[2] = cache[1];

        //pos y
        cache = DecToBin(pos.y);
        data[3] = cache[0];
        data[4] = cache[1];

        //pos z
        cache = DecToBin(pos.z);
        data[5] = cache[0];
        data[6] = cache[1];

        //rot x
        cache = DecToBin(rot.x);
        data[7] = cache[0];
        data[8] = cache[1];

        //rot y
        cache = DecToBin(rot.y);
        data[9] = cache[0];
        data[10] = cache[1];

        //rot z
        cache = DecToBin(rot.z);
        data[11] = cache[0];
        data[12] = cache[1];
    }

    public static byte[] DecToBin(double num)
    {
        int[] binary = new int[16];

        int egesz = (int)num;
        double tort = Mathf.Abs((float)num - (float)egesz);


        // +/-
        if (egesz < 0)
        {
            binary[0] = 0;
            egesz *= -1;
        }
        else
        {
            binary[0] = 1;
        }

        //dec to bin
        int i = 7;
        while (i > 0)
        {
            binary[i] = (int)(egesz % 2);
            egesz = (int)(egesz / 2);
            i--;
        }

        i = 8;
        while (i < binary.Length)
        {
            tort = tort * 2;

            if (tort >= 1)
            {
                binary[i] = 1;
                tort = tort - 1;
            }
            else
            {
                binary[i] = 0;
            }

            i++;
        }

        //bin to byte
        byte[] data = new byte[binary.Length / 8];

        int j = 0; //j a binary bitjeinek az indexe
        for (i = 0; i < data.Length; i++)
        {
            int cache = 0;

            //i a bytok indexe
            while (j < (i + 1) * 8)
            {
                cache *= 2;
                cache += binary[j];
                j++;
            }

            data[i] = (byte)cache;
        }

        string text = "";
        for (int x = 0; x < binary.Length; x++)
        {
            text += binary[x];
        }
        Debug.Log(text);

        return data;
    }
}
