using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float xRotation;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.parent.transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);

        xRotation -= Input.GetAxis("Mouse Y");
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        gameObject.transform.localRotation = Quaternion.Euler(xRotation, gameObject.transform.localRotation.eulerAngles.y, 0);
    }
}
