﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PhilMovement : MonoBehaviour
{

    [Header("Body")]
    public static GameObject player;
    public static GameObject hand;
    public static GameObject head;

    public float speed;
   
    public int health;
    public Text healthText;
    public string[] firstHealtItem;
    public bool animOn;

    private Rigidbody rb;
    private Animator anim;
    private Quaternion Rotation;
    private float viewRange = 1;

    private int animWalkingHash = Animator.StringToHash("Walking");
    private int animPickupHash = Animator.StringToHash("Pickup");

    private bool pickedUp;

    [Header("Damage")]
    public float flashspeed = 1f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool damaged;
    public Image damageImage;

    private bool firstTimeHealtItem = true;
    private bool walkingStair;

    void Awake()
    {
        damageImage.color = Color.clear;
        Rotation = this.transform.rotation;
        PhilMovement.player = this.gameObject;
        PhilMovement.hand = this.transform.FindChild("Armature").FindChild("Bone").FindChild("handik.R").FindChild("handik.R_end").FindChild("Hand").gameObject;
        PhilMovement.head = this.transform.FindChild("Armature").FindChild("Bone").FindChild("pelwas.001").FindChild("pelwas").FindChild("spine").FindChild("ribs").FindChild("neck").FindChild("head").FindChild("Glasses").gameObject;

        hand.transform.localScale = new Vector3(hand.transform.localScale.x / hand.transform.lossyScale.x, hand.transform.localScale.y / hand.transform.lossyScale.y, hand.transform.localScale.z / hand.transform.lossyScale.z);
        head.transform.localScale = new Vector3(head.transform.localScale.x / head.transform.lossyScale.x, head.transform.localScale.y / head.transform.lossyScale.y, head.transform.localScale.z / head.transform.lossyScale.z);
        Physics.gravity = new Vector3(0, -10, 0) * 9f;
    }

    void Start()
    {
        health = 100;
        damaged = false;
        player = this.gameObject;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashspeed * Time.deltaTime);
        setHealthText();

    }

    void FixedUpdate()
    {
        // Prefend moving if Dialogue window opend
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            Move();
        }
    }

    void LateUpdate()
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (hand.transform.childCount != 0)
            {
                if (pickedUp)
                {
                    GetPickUpInteraction();
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
        }
        else if (animOn)
        {
            GetInteraction();
        }
        else if (pickedUp)
        {
            if (Input.GetKeyUp("space")) PhilDialogue.Instance.ContinueDialogue();
        }
        else
        {
            pickedUp = true;
        }

        SwitchingItems();
        MovementAnimations();

    }

    void Move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // movement
        float y = rb.velocity.y;

        Vector3 movement = new Vector3(h, 0, v);

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
        // ensured obama will fall continiously
        if (walkingStair)
        {
            movement = new Vector3(h, v/3, v/2);
            movement = movement.normalized * speed;
        }
        else
        {
            movement = movement.normalized * speed;
            movement.Set(movement.x, rb.velocity.y, movement.z);
        }
        rb.velocity = movement;
    }

    void GetInteraction()
    {
        if (Input.GetKeyUp("space"))
        {
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
            DropItem();
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

    public static void DropItem()
    {
        hand.transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        hand.transform.DetachChildren();
        InventorySystem.Instance.SwitchInventoryImange();
        InventorySystem.Instance.SwitchHandImage();
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
            if (this.transform.FindChild("Inventory").childCount != 0) this.transform.FindChild("Inventory").GetChild(this.transform.FindChild("Inventory").childCount - 1).GetComponent<PickUpAble>().PlaceItemInHand(player);
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
            anim.SetTrigger(animPickupHash);
        }
        if (other.CompareTag("Continue Dialogue") && Input.GetKeyUp("space"))
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Health
        if (other.CompareTag("Health"))
        {
            if (firstTimeHealtItem)
            {
                PhilDialogue.Instance.AddNewDialogue(firstHealtItem);
                firstTimeHealtItem = false;
            }
            health = Min(health + 10, 100);
            Destroy(other.gameObject);//.SetActive(false);
        }

        if (other.CompareTag("Bullet"))
        {
            damaged = true;
            health = Max(health - 5, 0);
        }
        if (other.CompareTag("Stair"))
        {
            walkingStair = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stair"))
        {
            walkingStair = false;
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

    void MovementAnimations()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v != 0 || h != 0)
        {
            anim.SetBool(animWalkingHash, true);
        }
        if (v == 0 && h == 0)
        {
            anim.SetBool(animWalkingHash, false);
        }
        else if (PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            anim.SetBool(animWalkingHash, false);
        }
    }
}
