using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject healthbarPrefab;

    public int health = 100;
    public GameObject bar;

    //public HealthBar bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = Instantiate(healthbarPrefab, GameObject.Find("Canvas").transform);

        if (gameObject.name == "Player 1")
        {
            bar.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar hbar = (HealthBar)bar.GetComponent(typeof(HealthBar));
        hbar.value = (double)health / 100;
        hbar.target = gameObject;
    }
}
