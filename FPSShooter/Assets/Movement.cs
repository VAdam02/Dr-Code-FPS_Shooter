using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject character;
    public GameObject head;
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
    }
}
