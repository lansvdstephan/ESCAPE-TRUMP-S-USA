using UnityEngine;
using System.Collections;

public class PhilMovement : MonoBehaviour {

    public float speed;
    private NavMeshAgent playerAgent;
    private Rigidbody rb;
    private Quaternion Rotation = Quaternion.LookRotation(new Vector3(0,0,1));

    void Start ()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
   
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // movement
        Vector3 movement = new Vector3(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        
        // turning
        if (movement == new Vector3(0, 0, 0))
        {
            rb.MoveRotation(Rotation);
        }
        else
        {
            Rotation = Quaternion.LookRotation(movement.normalized);
            rb.MoveRotation(Rotation);
        }

        //Preforming Raycast and Interaction
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 2 ))
        {
            if (Input.GetKeyDown("space") && hit.transform.gameObject.CompareTag("Interactable Object"))
            {
                print("Interacted with object");
                hit.transform.gameObject.GetComponent<PhilInteractable>().Interact(this.gameObject);
            }
        }
    }
}
