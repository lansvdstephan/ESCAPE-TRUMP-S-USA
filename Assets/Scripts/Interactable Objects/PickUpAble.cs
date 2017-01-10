using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpAble : PhilInteractable {
	public string name;
	public string[] foundDialog;

    private Quaternion originalRotation;
    private Vector3 originalScale;

    void Awake()
    {
        originalRotation = this.transform.rotation;
        originalScale = this.transform.lossyScale;
    }


    public override void Interact(GameObject player)
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if ( this.foundDialog != null) PhilDialogue.Instance.AddNewDialogue(this.foundDialog);
			PlayerDataForServer.FoundItem (name);
            if (PhilMovement.hand.transform.childCount == 0)
            {
                PlaceItemInHand(player);
            }
            else
            {
                PlaceItemInFrontOfInventory(player);
                InventorySystem.Instance.SwitchInventoryImange();
            }
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
    }

    //Doing an action with a pickup item
    public virtual bool GetAction()
    {
        if (PhilDialogue.Instance.dialoguePanel.activeSelf)
        { 
            PhilDialogue.Instance.ContinueDialogue();
            return true;
        }
        return false;
    }

    //placing a object in your hand
    public void PlaceItemInHand(GameObject player)
    {
        this.transform.parent = PhilMovement.hand.transform;
        this.transform.position = PhilMovement.hand.transform.position;
        this.transform.localRotation = originalRotation;
        this.transform.localScale = originalScale;
        this.GetComponent<CapsuleCollider>().enabled = false;
		this.gameObject.SetActive (true);
        InventorySystem.Instance.SwitchHandImage();
        InventorySystem.Instance.SwitchInventoryImange();
    }

    //placing a object in back of your inventory
    public void PlaceItemInBackOfInventory(GameObject player)
    {
        this.transform.parent = player.transform.FindChild("Inventory").transform;
        this.transform.position = player.transform.FindChild("Inventory").transform.position;
        this.GetComponent<CapsuleCollider>().enabled = false;
		this.gameObject.SetActive (false);
        
    }

    //placing a object in front of your inventory
    public void PlaceItemInFrontOfInventory(GameObject player)
    {
        Transform Inventory = player.transform.FindChild("Inventory");
        this.GetComponent<CapsuleCollider>().enabled = false;
		this.gameObject.SetActive (false);
        this.transform.position = Inventory.transform.position;
        int children = Inventory.transform.childCount;
        List<Transform> Items = new List<Transform>(children);
        for (int i = 0; i < children; i++)
        {
            Items.Add(Inventory.transform.GetChild(i));
        }
        Inventory.transform.DetachChildren();
        this.transform.parent = Inventory.transform;
        for (int i = 0; i < children; i++)
        {
            Items[i].SetParent(Inventory.transform);
        }
        
    }
}
