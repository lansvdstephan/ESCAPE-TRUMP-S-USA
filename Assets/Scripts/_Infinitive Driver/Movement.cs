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
    private bool activated;

    public float flashspeed;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Color healthColor = new Color(0f, 249f, 249f, 0.1f);
    public Color fuelColor = new Color(0f, 255f, 8f, 0.1f);
    private Color invisable = new Color(255f, 255f, 255f, 0f);
    public Image damageImage;
    public bool damaged;
    private bool fuels;
    private bool healthje;


    void Start () {
        Physics.gravity = Physics.gravity * 9f;
        damageImage.color = invisable;
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0 || fuel <= 0)
        {
			this.gameObject.GetComponent<Animator> ().SetTrigger ("Crash");
        }
        if(damaged)
        {
            damageImage.color = flashColor;
            damaged = false;
        }
        if(fuels)
        {
            damageImage.color =fuelColor;
            fuels = false;
        }
        if(healthje)
        {
            damageImage.color = healthColor;
            healthje = false;
        }
        damageImage.color = Color.Lerp(damageImage.color, invisable, flashspeed * Time.deltaTime);
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
            // prefenting the tank to leave the road
            if (this.transform.position.x < -14f)
            {
                this.transform.position = new Vector3(-14f, this.transform.position.y, this.transform.position.z);
            }
            else if (this.transform.position.x > 4f)
            {
                this.transform.position = new Vector3(4f, this.transform.position.y, this.transform.position.z);
            }
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

        if (damageImage.color.Equals(Color.clear))
        {
            damageImage.color = flashColor;
        }

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
            fuels = true;
            
        }
        if (other.CompareTag("Health"))
        {
            this.health += 20;
            Destroy(other.gameObject);
            healthje = true;
        }
        if (other.gameObject.CompareTag("Obstackle"))
        {
            //winText.text = "You are Crashed";
            this.health = this.health - damageDoneByCollision;
            damaged = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstackle"))
        {
            winText.text = "You are Crashed";
            this.health = this.health - damageDoneByCollision;
            damaged = true;
        }

    }

	void GameOver(){
		GameObject.Find("MainMenuCanvas").transform.FindChild("UIManager").GetComponent<UIManager>().GameOver(true);
		print ("GameOver");
	}

}
