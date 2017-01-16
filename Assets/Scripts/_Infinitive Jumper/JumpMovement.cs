using UnityEngine;
using System.Collections;

public class JumpMovement : MonoBehaviour {
    public int speed;
    public float jumpForce;
    private Animator anim;
    private int animWalkingHash = Animator.StringToHash("Walking");
    private int animTakeOffHash = Animator.StringToHash("Take Off");
    private int animLandingHash = Animator.StringToHash("On Ground");
    private int animFallingHash = Animator.StringToHash("Falling");
    private Quaternion Rotation;
    private GameObject landingCube; 

    private Rigidbody rb;
    public bool onGround = true;
    private float verticalVelocity;
    private float pos;

    // Use this for initialization
    void Start()
    {
        Physics.gravity = Physics.gravity * 9f;
        rb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        Rotation = this.transform.rotation;
        landingCube = this.transform.FindChild("LandingCube").gameObject;
    }

    // Update is called once per frame
    void Update()
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
        landingCube.transform.localPosition=new Vector3(-0f,-0.50f+pos, 0.15f);
        landingCube.transform.localScale=new Vector3(0.5f, 0.75f-pos*2f, 2f);
        onGround = Mathf.Abs(verticalVelocity) <= 0.1;
        //anim.SetBool(animLandingHash, onGround);
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, 0);
        movement = movement * speed * Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
        {
            movement = movement/2;
        }
        this.transform.position = this.transform.position + movement;
        if (movement == new Vector3(0, 0, 0))
        {
            rb.MoveRotation(Rotation);
        }
        else
        {
            Rotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(Rotation);
        }
        anim.SetBool(animWalkingHash, h != 0);
        if (Input.GetKeyDown("up") && onGround && !anim.GetCurrentAnimatorStateInfo(0).IsName("Take Off"))
        {
            anim.SetTrigger(animTakeOffHash);
            StartCoroutine(Jump());
        }
    }

    public IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.084f);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);

    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * -0.5f, ForceMode.Acceleration);
    }


}
