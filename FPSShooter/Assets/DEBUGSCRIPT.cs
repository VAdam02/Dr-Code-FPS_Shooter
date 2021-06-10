using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGSCRIPT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        double x = gameObject.transform.position.x;
        byte[] asd = Connection.DecToBin(x);

        string text = "";
        for (int a = 0; a < asd.Length; a++)
        {
            text += asd[a] + " ";
        }

        Debug.Log(text + "\tX: " + x);
    }
}
