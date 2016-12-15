using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 1;
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(h, 0, 1);
        movement = movement.normalized * speed * Time.deltaTime;
        this.transform.position = this.transform.position + movement;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstackle"))
        {
            Destroy(this.gameObject);
        }
    }
}
