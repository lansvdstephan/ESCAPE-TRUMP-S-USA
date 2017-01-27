using UnityEngine;
using System.Collections;

public class Turnable : Switchable {
    public float openAngleX = 00f;
    public float closeAngleX = 0f;
    public float openAngleY = 90f;
    public float closeAngleY = 0f;
    public float openAngleZ = 0f;
    public float closeAngleZ = 0f;
    public float smooth = 2f;

	public override void SwitchOn()
    {
        transform.rotation = Quaternion.Euler(openAngleX, openAngleY, openAngleZ);
        //Quaternion rotation = Quaternion.Euler(0, openAngle, 0);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, smooth*Time.deltaTime); 
        if (this.GetComponent<AudioSource>() != null)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }

    public override void SwitchOff()
    {
        transform.rotation = Quaternion.Euler(openAngleX, closeAngleY, openAngleZ);
        if (this.GetComponent<AudioSource>() != null)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
}
