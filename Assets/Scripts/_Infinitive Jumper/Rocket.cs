using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Powerup {

    public override void Action()
    {
        JumpMovement.player.GetComponent<JumpMovement>().rocketOn = true;
    }
}
