using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadData(byte[] data)
    {
        
        float xpos = Connection.ByteToDec(new byte[] { data[1], data[2] });
        float ypos = Connection.ByteToDec(new byte[] { data[3], data[4] });
        float zpos = Connection.ByteToDec(new byte[] { data[5], data[6] });
        gameObject.transform.parent.position = new Vector3(xpos, ypos, zpos);

        float xrot = Connection.ByteToDec(new byte[] { data[7], data[8] });
        float yrot = Connection.ByteToDec(new byte[] { data[9], data[10] });
        float zrot = Connection.ByteToDec(new byte[] { data[11], data[12] });
        gameObject.transform.rotation = Quaternion.Euler(xrot, gameObject.transform.rotation.y, zrot);
        gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.parent.rotation.x, yrot, gameObject.transform.parent.rotation.z);
    }
}
