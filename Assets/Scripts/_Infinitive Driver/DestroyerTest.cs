using UnityEngine;
using System.Collections;

public class DestroyerTest : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            Destroy(other.gameObject.transform.parent);
        }
        else
        {
            Destroy(other.gameObject);
        }
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            Destroy(collision.gameObject.transform.parent);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
