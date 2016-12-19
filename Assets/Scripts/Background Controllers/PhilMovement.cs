﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PhilMovement : MonoBehaviour {
    public static GameObject player;
    public MoveonPath mop;
    public smartMoveOnPath smop;
    public float speed;
    private int health;
    public Text healthText;

    private Rigidbody rb;
    private GameObject hand;
	private Animator anim;
    private Quaternion Rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
    private float viewRange = 1;

	private int animWalkingHash = Animator.StringToHash("Walking");
    private int animPickupHash = Animator.StringToHash("Pickup");

    private bool pickedUp;


    void Awake()
    {
        player = this.gameObject;
        hand = this.transform.FindChild("Armature").FindChild("Bone").FindChild("handik.R").FindChild("handik.R_end").FindChild("Hand").gameObject;
    }

    void Start()
    {
        health = 100;
        player = this.gameObject;
        rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
    }

    void Update()
    {
        setHealthText();
    }

    void LateUpdate()
    {
        // Prefend moving if Dialogue window opend
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf) {
            Move();
        }
        if (hand.transform.childCount != 0)
        {
            if (pickedUp)
            {
                GetPickUpInteraction();
                PickupAnimations();
            }
            else
            {
                pickedUp = true;
            }
        }
        else
        {
            GetInteraction();
        }
        SwitchingItems();
		MovementAnimations ();
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
        if (Input.GetKeyUp("space")) {
            //Preforming Raycast and Interaction
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 Xas = new Vector3(1, 0, 0);
            Quaternion zeroPoint = Quaternion.Euler(Xas);
            Quaternion forwardRotation = Quaternion.Euler(fwd.normalized);
            RaycastHit hit;

            for (float i = -0.2f; i <= 0.2f; i = i + 0.1f)
            {
                for (float j = -0.2f; j <= 0.2f; j = j + 0.1f)
                {
                    Vector3 offset = new Vector3(i * Mathf.Sin(Quaternion.Angle(zeroPoint, forwardRotation)), j, i * Mathf.Cos(Quaternion.Angle(zeroPoint, forwardRotation)));
                    if (Physics.Raycast(transform.position + offset, fwd, out hit, viewRange))
                    {
                        if (hit.transform.gameObject.CompareTag("Interactable Object"))
                        {
                            print("Interacted with object");
                            hit.transform.gameObject.GetComponent<PhilInteractable>().Interact(player);
                            return;
                        }
                    }
                }
            }
        }

    }

    // Pick up item and keys
    void GetPickUpInteraction()
    {
        // Dropping item if left shift is pressed else doing action if space is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
           hand.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
           hand.transform.DetachChildren();
            InventorySystem.Instance.SwitchInventoryImange();
            InventorySystem.Instance.SwitchHandImage();

        }
        else if (Input.GetKeyUp("space"))
        {
            if (hand.transform.GetChild(0).GetComponent<PickUpAble>().GetAction())
            {
                return;
            }
            GetInteraction();
        }
    }

    private void SwitchingItems()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (hand.transform.childCount != 0) hand.transform.GetChild(0).GetComponent<PickUpAble>().PlaceItemInBackOfInventory(player);
            if (this.transform.FindChild("Inventory").childCount != 0) this.transform.FindChild("Inventory").GetChild(0).GetComponent<PickUpAble>().PlaceItemInHand(player);
            InventorySystem.Instance.SwitchInventoryImange();
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            if (hand.transform.childCount != 0) hand.transform.GetChild(0).GetComponent<PickUpAble>().PlaceItemInFrontOfInventory(player);
            if (this.transform.FindChild("Inventory").childCount != 0) this.transform.FindChild("Inventory").GetChild(this.transform.FindChild("Inventory").childCount-1).GetComponent<PickUpAble>().PlaceItemInHand(player);
            InventorySystem.Instance.SwitchInventoryImange();
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Picking up Items
        if (other.CompareTag("Pickup Item") && Input.GetKeyUp("space"))
        {
            print("Picked up item.");
            other.GetComponent<PhilInteractable>().Interact(player);
            pickedUp = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
        //Damage
        if (other.CompareTag("Enemy"))
        {
            health = Max(health - 20, 0);
            mop.hitPlayer = true;
            smop.hitPlayer = true;
        }

        //Health
        if (other.CompareTag("Health"))
        {
            health = Min(health + 10, 100);
            other.gameObject.SetActive(false);
        }
    }

    // Health
    void setHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    private int Max(int f1, int f2)
    {
        if (f1 > f2)
            return f1;
        else
            return f2;
    }

    private int Min(int f1, int f2)
    {
        if (f1 < f2)
            return f1;
        else
            return f2;
    }

	void MovementAnimations(){
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
		
		if (v != 0 || h != 0) {
			anim.SetBool (animWalkingHash, true);
		}
		if (v == 0 && h == 0) {
			anim.SetBool (animWalkingHash, false);
		}
		else if (PhilDialogue.Instance.dialoguePanel.activeSelf){
			anim.SetBool (animWalkingHash, false);
		}
	}

    void PickupAnimations()
    {
        anim.SetBool(animPickupHash, true);
        anim.SetBool(animPickupHash, false);
    }
}
