using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    float[] buffer = new float[7];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent.position = new Vector3(buffer[0], buffer[1], buffer[2]);
        gameObject.transform.localEulerAngles = new Vector3(buffer[3], gameObject.transform.rotation.y, buffer[5]);
        gameObject.transform.parent.localEulerAngles = new Vector3(gameObject.transform.parent.rotation.x, buffer[4], gameObject.transform.parent.rotation.z);
    }

    public void ReadData(byte[] data)
    {
        if (data[1] == 255)
        {
            EnemyJetpack enemyjetpack = (EnemyJetpack)gameObject.GetComponent(typeof(EnemyJetpack));
            if (data[2] == 1)
            {
                enemyjetpack.StartJetpack();
            }
            else
            {
                enemyjetpack.StopJetpack();
            }
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
