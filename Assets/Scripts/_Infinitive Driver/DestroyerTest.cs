using UnityEngine;
using System.Collections;

public class DestroyerTest : MonoBehaviour {
	
	void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Don't Destroy"))
        {
            if (collision.gameObject.transform.parent != null)
            {
                Destroy(collision.gameObject.transform.parent.gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
