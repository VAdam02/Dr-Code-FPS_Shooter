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
        int x = (int)gameObject.transform.position.x;
        Debug.Log("x: " + x + "\t" + Connection.DecToBin(x));
    }
}
