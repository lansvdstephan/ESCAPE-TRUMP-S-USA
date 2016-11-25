using UnityEngine;
using System.Collections;

public class Switchable : MonoBehaviour
{

    public virtual void SwitchOn()
    {
        print("Switch is on");
    }

    public virtual void SwitchOff()
    {
        print("Switch is off");
    }
}
