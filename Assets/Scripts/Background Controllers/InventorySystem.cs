using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySystem : MonoBehaviour {
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryPanel;

    private GameObject hand;
    private GameObject inventory;
    private Image handImage;
    private Image[] inventoryImage;
    private int numberOfImages;

    void Start()
    {
        hand = PhilMovement.hand;
        inventory = PhilMovement.player.transform.FindChild("Inventory").gameObject;
        numberOfImages = inventoryPanel.transform.childCount - 1;
        inventoryImage = new Image[numberOfImages];
        handImage = inventoryPanel.transform.FindChild("Hand Item").GetComponent<Image>();
        for (int i = 0; i < numberOfImages; i++)
        {
            inventoryImage[i] = inventoryPanel.transform.FindChild("Inventory Item" + i).GetComponent<Image>();
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SwitchHandImage()
    {
		if (hand.transform.childCount != 0) {
			handImage.sprite = hand.transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ().sprite;
		} else {
			handImage.sprite = null;
		}
    }

    public void SwitchInventoryImange()
    {
        int InventorySize = inventory.transform.childCount;
        if (InventorySize > numberOfImages) InventorySize = numberOfImages;
        if (InventorySize > 0)
        {
            for (int i = 0; i < InventorySize; i++)
            {

                inventoryImage[i].sprite = inventory.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;

            }
        }
        else
        {
            for (int i = 0; i < numberOfImages; i++)
            {

                inventoryImage[i].sprite = null;

            }
        }

    }

}
