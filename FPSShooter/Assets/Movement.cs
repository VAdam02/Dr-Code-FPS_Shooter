using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject character;
    public GameObject head;
    public GameObject arm;
    public Camera center;
    public Camera scopecam;
    public GameObject bullet;
    public GameObject lookintothedistant;
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
        if (Physics.Raycast(ray, out hit, 25))
        {
            Vector3 target = hit.point;
            arm.transform.LookAt(target);
        }
        else
        {
            arm.transform.LookAt(lookintothedistant.transform);
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

            shot.transform.position += shot.transform.forward *2.8f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            center.enabled = false;
            scopecam.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            center.enabled = true;
            scopecam.enabled = false;
        }
    }
}
