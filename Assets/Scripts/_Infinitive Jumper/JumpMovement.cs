using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class JumpMovement : MonoBehaviour {
    public static GameObject player;
    public float speed;
    public float jumpForce;
    public int health = 100;
    public float goal = 500;

    [Header("Visual Effects")]
    public float flashspeed = 0.01f;
    public Text healthText;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private Color invisable = new Color(255f, 255f, 255f, 0f);
    public Image damageImage;
    public bool damaged;

    [Header("Power Ups")]
    public bool rocketOn;
    public float rocketTime = 5f;
    public float shieldTime = 5f;

    [Header("No need for implementation")]
    public Vector3 correctedSpeed;
    private GameObject UIManager;
    private Animator anim;
    private int animWalkingHash = Animator.StringToHash("Walking");
    private int animTakeOffHash = Animator.StringToHash("Take Off");
    private int animLandingHash = Animator.StringToHash("On Ground");
    private int animFallingHash = Animator.StringToHash("Falling");
    private Quaternion rotation;
    private GameObject landingCube;
    private float verticalVelocity;
    private float pos;

    private Rigidbody rb;
    public bool onGround = true;
    private float rocketTimer = -1f;
    private float shieldTimer = -1f;

    public GameObject levelCompletedPanel;

    void Awake()
    {
        player = this.gameObject;
        if (GameObject.FindWithTag("UIManager") != null)
        {
            UIManager = GameObject.FindWithTag("UIManager").gameObject;
			levelCompletedPanel = GameObject.Find ("MainMenuCanvas").gameObject.transform.FindChild ("Level Completed Panel").gameObject;
        }

    }

    void Start()
    {

        Physics.gravity = new Vector3(0, -10, 0) * 9f;
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rotation = this.transform.rotation;
        landingCube = this.transform.FindChild("LandingCube").gameObject;
        damageImage.color = invisable;
    }

    void Update()
    {
        if (PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (Input.GetKeyUp("space"))
            {
                PhilDialogue.Instance.ContinueDialogue();
            }
        }
        else
        {
            if (this.transform.position.y > goal)
            {
                LoadWinningScreen();
            }

            SetHealthText();

            JumpAnimation();

            Move();

            PowerUpControl();

            if (damaged)
            {
                damageImage.color = flashColor;
                damaged = false;
            }
            damageImage.color = Color.Lerp(damageImage.color, invisable, flashspeed * Time.deltaTime);
        }
        if (health <= 0)
        {UIManager.GetComponent<UIManager>().GameOver(true);}
    }

    void OnDestroy()
    {

    }
    private void Move()
    {
        //anim.SetBool(animLandingHash, onGround);
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, 0);
        movement = movement * speed;
       
        if (movement == new Vector3(0, 0, 0))
        {
            rb.MoveRotation(rotation);
        }
        else
        {
            rotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(rotation);
        }
        anim.SetBool(animWalkingHash, h != 0);
        if (Input.GetKeyDown("up") && onGround && !anim.GetCurrentAnimatorStateInfo(0).IsName("Take Off"))
        {
            anim.SetTrigger(animTakeOffHash);
            StartCoroutine(Jump());
        }
        else if (rocketOn && rocketTimer > 0)
        {
            rb.velocity = 25f * Vector3.up;
            if (rocketTimer == rocketTime)
            {
                this.gameObject.layer = 13;
                this.transform.FindChild("Cylinder").gameObject.layer = 13;
            }
            rocketTimer = rocketTimer - 1 * Time.deltaTime;
        }
        movement.Set(movement.x, rb.velocity.y, movement.z);
        rb.velocity = movement;
        correctedSpeed = movement;
    }

    private void JumpAnimation()
    {
        verticalVelocity = rb.velocity.y;
        if (verticalVelocity < -0.1f)
        {
            anim.SetBool(animFallingHash, true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
        {
            anim.SetBool(animFallingHash, false);
        }
        pos = verticalVelocity / 25f;
        if (pos > 0) { pos = 0; }
        landingCube.transform.localPosition = new Vector3(-0f, -0.50f + pos, 0.15f);
        landingCube.transform.localScale = new Vector3(0.5f, 0.75f - pos * 2f, 2f);
        onGround = Mathf.Abs(verticalVelocity) <= 0.1;
    }

    private void PowerUpControl()
    {
        if (rocketTimer < 0)
        {
            rocketOn = false;
            if (rb.velocity.y < 0)
            {
                rocketTimer = rocketTime;
                this.gameObject.layer = 1;
                this.transform.FindChild("Cylinder").gameObject.layer = 1;
            }
        }

        if (player.transform.FindChild("ActiveShield").gameObject.activeSelf && shieldTimer > 0)
        {
            shieldTimer = shieldTimer - 1 * Time.deltaTime;
        }
        if (shieldTimer <= 0)
        {
            shieldTimer = shieldTime;
            player.transform.FindChild("ActiveShield").gameObject.SetActive(false);
        }
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.10f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);

    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * -0.5f, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup Item"))
        {
            other.GetComponent<Powerup>().Action();
            Destroy(other.gameObject);
        }
    }

    private void SetHealthText()
    {
        healthText.text = "Health:" + health.ToString();
    }

    private void LoadWinningScreen()
    {
        Time.timeScale = 0.0f;
        string timeLeftString = GameObject.Find("CountdownText").gameObject.transform.FindChild("TimeText").GetComponent<Text>().text;
        float timeLeft = GameObject.Find("CountdownKeeper").GetComponent<CountDown>().tijd;
        int healthLeft = health;

        levelCompletedPanel.GetComponent<CalculateScore>().timeBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().healthBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().timeLeft = (int)timeLeft;
        levelCompletedPanel.GetComponent<CalculateScore>().health = healthLeft;
        levelCompletedPanel.SetActive(true);
    }
}
