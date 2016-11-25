using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySystem : MonoBehaviour {
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryPanel;

    private Image handImange;
    private Image[] inventoryImage;
    private Image inventory1Image;
    private Image inventory2Image;

    void Awake()
    {
        inventoryImage = new Image[3];
        handImange = inventoryPanel.transform.FindChild("Hand Item").GetComponent<Image>();
        inventoryImage[0] = inventoryPanel.transform.FindChild("Inventory Item").GetComponent<Image>();
        inventoryImage[1] = inventoryPanel.transform.FindChild("Inventory Item (1)").GetComponent<Image>();
        inventoryImage[2] = inventoryPanel.transform.FindChild("Inventory Item (2)").GetComponent<Image>();

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
        if (InventorySize > 3) InventorySize = 3;
        if (InventorySize > 0)
        {
            for (int i = 0; i < InventorySize; i++)
            {
                inventoryImage[i].sprite = PhilMovement.player.transform.FindChild("Inventory").GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().sprite;
            }
        }
    }

}
