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
    public GameObject ammo;

    public static DataReceiver[] enemys = new DataReceiver[256];
    public static bool[] spawned = new bool[enemys.Length];

    public static Thread reader;
    static bool reading;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawned.Length; i++) { spawned[i] = false; }

        //client = new TcpClient("127.0.0.1", 50000);
        //client = new TcpClient("2a01:36d:1600:bec:f97a:5031:824b:75c6", 55000);
        client = new TcpClient("192.168.100.129", 55000);
        stream = client.GetStream();

        reader = new Thread(() => ReadData());
        reader.Start();
    }

    // Update is called once per frame
    void Update()
    {
        while (spawnqueue.Count > 0)
        {
            byte[] data = spawnqueue[0];

            if (data[0] == 255)
            {
                GameObject xy = Instantiate(ammo);
                xy.transform.position = new Vector3(ByteToDec(new byte[] { data[1], data[2] }), ByteToDec(new byte[] { data[3], data[4] }), ByteToDec(new byte[] { data[5], data[6] }));
                xy.transform.localEulerAngles = new Vector3(ByteToDec(new byte[] { data[7], data[8] }) * 3, ByteToDec(new byte[] { data[9], data[10] }) * 3, ByteToDec(new byte[] { data[11], data[12] }) * 3);
            }
            else if (!spawned[data[0]])
            {
                GameObject xy = Instantiate(enemy);
                spawned[data[0]] = true;
                enemys[data[0]] = xy.gameObject.transform.GetChild(0).GetComponent<DataReceiver>();
                enemys[data[0]].ReadData(data);
            }

            spawnqueue.RemoveAt(0);
        }
    }

    static List<byte[]> spawnqueue = new List<byte[]>();

    public static void ReadData()
    {
        reading = true;
        while (reading)
        {
            byte[] data = new byte[13];
            stream.Read(data, 0, data.Length);

            if (!spawned[data[0]])
            {
                spawnqueue.Add(data);
            }
            else
            {
                enemys[data[0]].ReadData(data);
            }
        }
    }

    public static void WriteData(byte[] data)
    {
        stream.Write(data, 0, data.Length);
    }
    public static void WriteData(byte id, Vector3 pos, Vector3 rot)
    {
        byte[] data = new byte[13];

        data[0] = id;

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

        WriteData(data);
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

        return data;
    }

    public static float ByteToDec(byte[] data)
    {
        int[] binary = new int[data.Length * 8];

        //byte to bin
        for (int i = 0; i < data.Length; i++)
        {
            int num = data[i];
            for (int j = 7; j >= 0; j--)
            {
                binary[j + (i * 8)] = (int)(num % 2);
                num = (int)(num / 2);
            }
        }

        //bin to num
        float eredmeny = 0;

        for (int i = binary.Length - 1; i > 7; i--)
        {
            eredmeny = (eredmeny + binary[i]) / 2;
        }

        int cache = 0;
        for (int i = 1; i < 8; i++)
        {
            cache = (cache * 2) + binary[i];
        }
        eredmeny = eredmeny + cache;

        //+/-
        if (binary[0] == 0)
        {
            eredmeny = eredmeny * (-1);
        }

        return eredmeny;
    }
}
