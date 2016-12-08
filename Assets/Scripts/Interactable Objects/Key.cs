using UnityEngine;
using System.Collections;

public class Key : PickUpAble {

    public int keyCode = 00;

    public override bool GetAction()
    {
        if (base.GetAction())
        {
            return true;
        }
        print("you a ass");
        return false;
    }
}
