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
    public Sprite foundSprite;

    protected GameObject playerHand;
    protected GameObject player;
    private bool givedItem = false;

    void Start () {
        playerHand = PhilMovement.hand;
        player = PhilMovement.player;

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
                if (foundSprite != null)
                {
                    PhilDialogue.Instance.AddNewDialogue(this.foundDialog, foundSprite);
                }
                else
                {
                    PhilDialogue.Instance.AddNewDialogue(this.foundDialog, dialogueSprite);
                }
            }
            else if (playerHand.transform.childCount != 0)
            {
                if (playerHand.transform.GetChild(0).GetComponent<PickUpAble>().name.Equals(searchItem))
                {

                    givedItem = true;
                    PlaceItem(playerHand.transform.GetChild(0).gameObject);
                    PhilDialogue.Instance.AddNewDialogue(this.rightItem, dialogueSprite);
                }
                else if (searchItem.Equals("None"))
                {

                    givedItem = true;
                    PlaceItem(null);
                    PhilDialogue.Instance.AddNewDialogue(this.rightItem, dialogueSprite);
                }
                else
                {
                    PhilDialogue.Instance.AddNewDialogue(this.wrongItem, dialogueSprite);
                }
            }
            else if (searchItem.Equals("None"))
            {
                givedItem = true;
                PlaceItem(null);
                PhilDialogue.Instance.AddNewDialogue(this.rightItem, dialogueSprite);
            }
            else
            {
                PhilDialogue.Instance.AddNewDialogue(this.searchDialog, dialogueSprite);
            }
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
	}

    private void PlaceItem(GameObject Item)
    {
        if (Item != null)
        {
            PhilMovement.DropItem();
            Item.transform.position = itemPlace.position;
            Item.transform.localRotation = Item.GetComponent<PickUpAble>().GetRotation();
            Item.transform.localScale = Item.GetComponent<PickUpAble>().GetScale();
            Item.GetComponent<CapsuleCollider>().enabled = false;
        }
        ItemInteract();
    }

    public virtual void ItemInteract()
    {
        print("interacting with GiveItem");
    }
}
