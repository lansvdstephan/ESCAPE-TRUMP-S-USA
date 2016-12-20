using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour {
    public float speed = 1;

    private float fuel = 10;
    public Text fuelText;
	
	// Update is called once per frame
	void Update () {
        SetFuelText();
        if (fuel > 0)
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(h, 0, 1);
            movement = movement * speed * Time.deltaTime;
            this.transform.position = this.transform.position + movement;
            fuel -= Time.deltaTime * 2;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SetFuelText()
    {
        fuelText.text = "Fuel:" + fuel.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstackle"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Fuel"))
        {
            this.fuel += 10;
            Destroy(other.gameObject);
        }
    }
}
