﻿using UnityEngine;
using System.Collections;

public class Key : PickUpAble {

    public int keyCode = 00;

    public override void GetAction()
    {
        print("keying");
    }
}
