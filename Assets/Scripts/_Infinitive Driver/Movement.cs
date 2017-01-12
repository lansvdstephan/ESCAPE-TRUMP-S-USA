using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Movement : MonoBehaviour {
    public float speed = 1;
    public float correctedSpeed;
    public float health = 100;
    public float damageDoneByCollision = 10;
    public float fuel = 100;

    [Header("Texts")]
    public Text fuelText;
    public Text winText;
    public Text healthText;

    public float points;

	void Start () {
		Physics.gravity = Physics.gravity * 9f;
	}

	// Update is called once per frame
	void Update () {
        SetFuelText();
        SetHealthText();
        if (fuel > 0 && health > 0)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(h, 0, 2 + v);
            correctedSpeed = speed * (2 + v);
            movement = movement * speed * Time.deltaTime;
            this.transform.position = this.transform.position + movement;
            fuel -= Time.deltaTime * 2;
        }
        else
        {
            winText.text = "You are Crashed";
            this.enabled = false;
            correctedSpeed = 0;
        }
        points = this.transform.position.z;
        winText.text = points.ToString();
        
    }

    private void SetFuelText()
    {
        fuelText.text = "Fuel:" + fuel.ToString();
    }

    private void SetHealthText()
    {
        healthText.text = "Health:" + health.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fuel"))
        {
            this.fuel += 10;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Health"))
        {
            this.health += 20;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Obstackle"))
        {
            //winText.text = "You are Crashed";
            this.health = this.health - damageDoneByCollision;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstackle"))
        {
            winText.text= "You are Crashed";
            this.health = this.health - damageDoneByCollision;
        }
    }

}
