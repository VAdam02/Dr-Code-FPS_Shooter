using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject character;
    public GameObject head;
    public GameObject arm;
    public Camera center;
    public GameObject bullet;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private float xRotation;
    // Update is called once per frame
    void Update()
    {
        character.transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);

        xRotation -= Input.GetAxis("Mouse Y");
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        head.transform.localRotation = Quaternion.Euler(xRotation, head.transform.localRotation.eulerAngles.y, 0);

        Ray ray = center.ScreenPointToRay(new Vector3(Screen.width /2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Vector3 target = hit.point;
            arm.transform.LookAt(target);
        }
        
        float multiplier = 5;
        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * multiplier;
        }
        if (Input.GetKey("s"))
        {
            transform.position += -transform.forward * Time.deltaTime * multiplier;
        }
        if (Input.GetKey("d"))
        {
            transform.position += transform.right * Time.deltaTime * multiplier;
        }
        if (Input.GetKey("a"))
        {
            transform.position += -transform.right * Time.deltaTime * multiplier;
        }
        if (Input.GetKey("space"))
        {
            rb.AddForce(0, 100, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(bullet);
            shot.transform.position = arm.transform.position;
            shot.transform.rotation = arm.transform.rotation;

            shot.transform.position += shot.transform.forward;
        }
    }
}
