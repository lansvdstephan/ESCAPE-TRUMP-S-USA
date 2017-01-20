using UnityEngine;
using System.Collections;

public class TradeItem : GiveItem {

    [Header("Return Items")]
    public GameObject returnItem;
    public Transform returnPosition;

    public override void ItemInteract()
    {
        print("dropped returnItem");
        Instantiate(returnItem).transform.position = returnPosition.position; ;
    }
}
