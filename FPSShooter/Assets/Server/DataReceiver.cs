﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    public Health hp;
    float[] buffer = new float[7];
    public int ID;

    public int HUE = 0;
    Material mat;

    /*
     * 0    stop flying
     * 1    idle
     * 2    start flying
     */
    int fly = 1;

    bool live = true;

    // Start is called before the first frame update
    void Start()
    {
        mat = new Material(Shader.Find("Specular"));
        gameObject.transform.parent.GetComponent<Renderer>().material = mat;
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material = mat;

        hp = (Health)gameObject.transform.parent.GetComponent(typeof(Health));
    }

    // Update is called once per frame
    void Update()
    {
        if (!live)
        {
            Connection.spawned[ID] = false;
            Destroy(hp.bar);
            Destroy(gameObject.transform.parent.gameObject);
        }


        if (fly != 1)
        {
            mat.color = Color.HSVToRGB((float)HUE /255, 1f, 1f);

            EnemyJetpack enemyjetpack = (EnemyJetpack)gameObject.transform.parent.GetChild(2).GetComponent(typeof(EnemyJetpack));

            if (fly == 2)
            {
                enemyjetpack.StartJetpack();
            }
            else
            {
                enemyjetpack.StopJetpack();
            }

            fly = 1;
        }

        gameObject.transform.parent.position = new Vector3(buffer[0], buffer[1], buffer[2]);
        gameObject.transform.localEulerAngles = new Vector3(buffer[3], gameObject.transform.rotation.y, buffer[5]);
        gameObject.transform.parent.localEulerAngles = new Vector3(gameObject.transform.parent.rotation.x, buffer[4], gameObject.transform.parent.rotation.z);
    }

    public void ReadData(byte[] data)
    {
        if (data[1] == 255)
        {
            if (data[2] == 1)
            {
                fly = 2;
            }
            else
            {
                fly = 0;
            }

            HUE = data[4];
            hp.health = data[3];
        }
        else if (data[1] == 254)
        {
            live = false;
        }
        else
        {
            buffer[0] = Connection.ByteToDec(new byte[] { data[1], data[2] });
            buffer[1] = Connection.ByteToDec(new byte[] { data[3], data[4] });
            buffer[2] = Connection.ByteToDec(new byte[] { data[5], data[6] });

            buffer[3] = Connection.ByteToDec(new byte[] { data[7], data[8] }) * 3;
            buffer[4] = Connection.ByteToDec(new byte[] { data[9], data[10] }) * 3;
            buffer[5] = Connection.ByteToDec(new byte[] { data[11], data[12] }) * 3;
        }
    }
}
