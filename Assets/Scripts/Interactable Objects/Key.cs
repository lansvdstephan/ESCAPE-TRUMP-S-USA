using UnityEngine;
using System.Collections;

public class Key : PickUpAble {

    public override void GetAction()
    {
        base.GetAction();
        //Preforming Raycast and Interaction
        print("Interacted");
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            if (hit.transform.gameObject.CompareTag("Interactable Object"))
            {
                if (hit.transform.GetComponent<SwitchController>() != null)
                {
                    hit.transform.GetComponent<SwitchController>().unlocked = true;
                }
            }
        }
    }
}
