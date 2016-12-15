using UnityEngine;
using System.Collections;

public class DestroyerTest : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
    {
        print("stupid ass");
        if (other.gameObject.transform.parent != null)
        {
            Destroy(other.gameObject.transform.parent);
        }
        else
        {
            Destroy(other.gameObject);
        }
	
	}
}
