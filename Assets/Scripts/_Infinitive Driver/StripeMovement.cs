using UnityEngine;
using System.Collections;

public class StripeMovement : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, 1);
        movement = movement.normalized * speed * Time.deltaTime;
        this.transform.position = this.transform.position + movement;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
    }
}
