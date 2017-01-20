using UnityEngine;
using System.Collections;

public class ChangeStileGiveItem : GiveItem {

    [Header("Return")]
    public GameObject sunGlasses;

    private Quaternion originalRotation;
    private Vector3 originalScale;

    void Awake()
    {
        originalRotation = sunGlasses.transform.rotation;
        originalScale = sunGlasses.transform.lossyScale;
    }

    public override void ItemInteract()
    {
        print("ChangedStyle");
        sunGlasses.transform.parent = PhilMovement.head.transform;
        sunGlasses.transform.position = PhilMovement.head.transform.position;
        sunGlasses.transform.localRotation = originalRotation;
        sunGlasses.transform.localScale = originalScale;
    }
}
