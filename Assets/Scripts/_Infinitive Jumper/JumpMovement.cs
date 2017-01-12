using UnityEngine;
using System.Collections;

public class JumpMovement : MonoBehaviour {
    public int speed;
    public float jumpForce;

    private Rigidbody rb;
    private bool onGround = true;

    // Use this for initialization
    void Start () {
        Physics.gravity = Physics.gravity * 9f;
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, 0);
        movement = movement * speed * Time.deltaTime;
        this.transform.position = this.transform.position + movement;
        if (Input.GetKeyDown("up")&& onGround)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * -0.5f, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) onGround = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) onGround = false;
    }
}
