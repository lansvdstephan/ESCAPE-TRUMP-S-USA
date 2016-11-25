using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySystem : MonoBehaviour {
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryPanel;

    private Image handImange;
    private Image[] inventoryImage;
    private int numberOfImages;

    void Awake()
    {
        numberOfImages = inventoryPanel.transform.childCount - 1;
        inventoryImage = new Image[numberOfImages];
        handImange = inventoryPanel.transform.FindChild("Hand Item").GetComponent<Image>();
        for (int i = 0; i < numberOfImages; i++)
        {
            inventoryImage[0] = inventoryPanel.transform.FindChild("Inventory Item" + i).GetComponent<Image>();
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

    public void SwitchHandImange()
    {
        handImange.sprite = PhilMovement.player.transform.FindChild("Hand").GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        print("image switched");
    }

    public void SwitchInventoryImange()
    {
        int InventorySize = PhilMovement.player.transform.FindChild("Inventory").childCount;
        if (InventorySize > numberOfImages) InventorySize = numberOfImages;
        if (InventorySize > 0)
        {
            for (int i = 0; i < InventorySize; i++)
            {
                inventoryImage[i].sprite = PhilMovement.player.transform.FindChild("Inventory").GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

}
