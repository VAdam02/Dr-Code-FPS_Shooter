using System;
using UnityEngine;

public class ArmLook : MonoBehaviour
{
    public Camera maincamera;
    public GameObject lookintothedistant;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //kelleni fog
        //kéz mozgatás
        Ray ray = maincamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 25))
        {
            Vector3 target = hit.point;
            gameObject.transform.LookAt(target);
        }
        else
        {
            gameObject.transform.LookAt(lookintothedistant.transform);
        }
        //kéz mozgatás
    }
}
