using UnityEngine;
using System.Collections;
using System;

public class PhilMovement : MonoBehaviour {
    public static GameObject player;
    public float speed;
    private Rigidbody rb;
    private Quaternion Rotation = Quaternion.LookRotation(new Vector3(0,0,1));

    void Awake()
    {
        player = this.gameObject;
    }

    void Start ()
    {
        player = this.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        Move();

        if(player.transform.FindChild("Hand").childCount != 0)
        {
            GetPickUpInteraction();
            SwitchingItems();
        }
        else
        {
            GetInteraction();
        }

    }

    void Move()
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
    }

    void GetInteraction()
    {
        //Preforming Raycast and Interaction
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            if (Input.GetKeyUp("space") && hit.transform.gameObject.CompareTag("Interactable Object"))
            {
                print("Interacted with object");
                hit.transform.gameObject.GetComponent<PhilInteractable>().Interact(player);
            }
        }
    }

    void GetPickUpInteraction()
    {
        // Dropping item if left shift is pressed else doing action if space is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            this.transform.FindChild("Hand").GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
            this.transform.FindChild("Hand").DetachChildren();
        }
        else if (Input.GetKeyUp("space"))
        {
            GetInteraction();
            this.transform.FindChild("Hand").GetChild(0).GetComponent<PickUpAble>().GetAction();
        }
    }

    private void SwitchingItems()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.transform.FindChild("Hand").GetChild(0).GetComponent<PickUpAble>().PlaceItemInBackOfInventory(player);
            this.transform.FindChild("Inventory").GetChild(0).GetComponent<PickUpAble>().PlaceItemInHand(player);
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            this.transform.FindChild("Hand").GetChild(0).GetComponent<PickUpAble>().PlaceItemInFrontOfInventory(player);
            this.transform.FindChild("Inventory").GetChild(this.transform.FindChild("Inventory").childCount-1).GetComponent<PickUpAble>().PlaceItemInHand(player);
        }
    }
}
