using UnityEngine;
using System.Collections;

public class GiveItemInInventory : GiveItem {

    [Header("Dialogue")]
    public GameObject returnItem;

    public override void ItemInteract()
    {
        returnItem.GetComponent<PickUpAble>().PlaceItemInBackOfInventory(PhilMovement.player);
    }
}
