using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    public double value = 0;

    int max = 73;
    GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        cam = (Camera)GameObject.Find("Player 1").transform.GetChild(0).GetChild(1).GetComponent(typeof(Camera));
        bar = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform rect = (RectTransform)bar.GetComponent(typeof(RectTransform));
        int size = (int)(max * value);
        int pos = (max - size) / -2;
        rect.sizeDelta = new Vector2(size, 7);
        rect.localPosition = new Vector3(pos, 0, 0);

        Vector3 screenpoint = cam.WorldToScreenPoint(target.transform.position + new Vector3(0, 1, 0));
        RectTransform rect2 = (RectTransform)gameObject.GetComponent(typeof(RectTransform));
        rect2.position = screenpoint;
    }
}
