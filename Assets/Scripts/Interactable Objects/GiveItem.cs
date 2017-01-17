using UnityEngine;
using System.Collections;
using System;

public class GiveItem : PhilInteractable {

    public string searchItem;
    public Transform itemPlace;

    [Header("Dialogue")]
    public string[] searchDialog;
    public string[] wrongItem;
    public string[] rightItem;
    public string[] foundDialog;

    private GameObject playerHand;
    private bool givedItem = false;

    void Start () {
        playerHand = PhilMovement.hand;

        if (searchDialog.Length == 0)
        {
            searchDialog = new string[1];
            searchDialog[0] = "Money for nothing.";
        }
        if (foundDialog.Length == 0)
        {
            foundDialog = new string[1];
            foundDialog[0] = "Play your gitar on the MTV.";
        }
        if (wrongItem.Length == 0)
        {
            wrongItem = new string[1];
            wrongItem[0] = "That ain't working.";
        }
        if (rightItem.Length == 0)
        {
            rightItem = new string[1];
            rightItem[0] = "That's the way you do it.";
        }
    }

    public override void Interact(GameObject player) { 
	    if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (givedItem)
            {
                PhilDialogue.Instance.AddNewDialogue(this.foundDialog);
            }
            else if (playerHand.transform.childCount != 0)
            {
                if (playerHand.transform.GetChild(0).GetComponent<PickUpAble>().name.Equals(searchItem))
                {
                    PhilDialogue.Instance.AddNewDialogue(this.rightItem);
                    givedItem = true;
                    PlaceItem(playerHand.transform.GetChild(0).gameObject);
                }
                else
                {
                    PhilDialogue.Instance.AddNewDialogue(this.wrongItem);
                }
            }
            else
            {
                PhilDialogue.Instance.AddNewDialogue(this.searchDialog);
            }
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
	}

    private void PlaceItem(GameObject Item)
    {
        PhilMovement.DropItem();
        Item.transform.position = itemPlace.position;
        Item.transform.localRotation = Item.GetComponent<PickUpAble>().GetRotation();
        Item.transform.localScale = Item.GetComponent<PickUpAble>().GetScale();
        Item.GetComponent<CapsuleCollider>().enabled = false;
        ItemInteract();
    }

    public virtual void ItemInteract()
    {
        print("interacting with GiveItem");
    }
}
