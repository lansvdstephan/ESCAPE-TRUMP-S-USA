using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup {

    public override void Action()
    {
        print("Shield");
        JumpMovement.player.transform.FindChild("ActiveShield").gameObject.SetActive(true);
    }
}
