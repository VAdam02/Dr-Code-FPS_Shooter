using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject maxZoom;
    public GameObject minZoom;
    public GameObject maincam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxZoom.transform.LookAt(minZoom.transform);
        RaycastHit hit;

        if (Physics.Raycast(maxZoom.transform.position, maxZoom.transform.TransformDirection(Vector3.forward), out hit, Vector3.Distance(maxZoom.transform.position, minZoom.transform.position)))
        {
            maincam.transform.position = Vector3.MoveTowards(hit.point, maxZoom.transform.position, 0.1f);
        }
        else
        {
            maincam.transform.position = minZoom.transform.position;
        }
    }
}
