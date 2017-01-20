using UnityEngine;
using System.Collections;

public class GiveItemInInventory : GiveItem {

    [Header("Dialogue")]
    public GameObject returnItem;

    public override void ItemInteract()
    {
        print("foo");
        if (playerHand.transform.childCount != 0) playerHand.transform.GetChild(0).GetComponent<PickUpAble>().PlaceItemInBackOfInventory(player);
        if (returnItem != null ) returnItem.GetComponent<PickUpAble>().PlaceItemInHand(player);
        InventorySystem.Instance.SwitchInventoryImange();
    }
}
