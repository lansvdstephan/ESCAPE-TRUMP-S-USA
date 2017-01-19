using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShield : MonoBehaviour {



    void OnCollisionExit(Collision collision)
    {
        print("beam");
        if (collision.transform.parent != null)
        {
            Destroy(collision.transform.parent.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
        this.gameObject.SetActive(false);

    }
}
