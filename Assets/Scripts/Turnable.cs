using UnityEngine;
using System.Collections;

public class Turnable : Switchable {
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float smooth = 2f;

	public override void SwitchOn()
    {
        transform.rotation = Quaternion.Euler(0, openAngle, 0);
        //Quaternion rotation = Quaternion.Euler(0, openAngle, 0);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, smooth*Time.deltaTime);
    }

    public override void SwitchOff()
    {
        transform.rotation = Quaternion.Euler(0, closeAngle, 0);
    }
}
