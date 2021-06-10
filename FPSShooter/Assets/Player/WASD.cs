using UnityEngine;

public class WASD : MonoBehaviour
{
    public Camera scopecam;
    public Camera maincam;
    public GameObject arm;
    public GameObject bullet;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //nem kell
        //wasd space
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
        //wasd space

        //nem fog kelleni
        //lövöldözés
        if (Input.GetMouseButtonDown(0))
        {
            GameObject shot = Instantiate(bullet);
            shot.transform.position = arm.transform.position;
            shot.transform.rotation = arm.transform.rotation;

            shot.transform.position += shot.transform.forward * 2.8f;

            Connection.WriteData(255, shot.transform.position, new Vector3(shot.transform.eulerAngles.x / 3, shot.transform.eulerAngles.y / 3, shot.transform.eulerAngles.z / 3));
        }
        //lövöldözés

        //nem fog kelleni
        //scope
        if (Input.GetMouseButtonDown(1))
        {
            maincam.enabled = false;
            scopecam.enabled = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            maincam.enabled = true;
            scopecam.enabled = false;
        }
        //scope
    }
}
