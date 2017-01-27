using UnityEngine;
using System.Collections;

public class DestroyerTest : MonoBehaviour
{

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            if (other.transform.parent.CompareTag("player"))
            {
                if (other.transform.parent.GetComponent<JumpMovement>() != null)
                {
                    other.gameObject.transform.parent.GetComponent<JumpMovement>().health = 0;
                }
            }
            else
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
        }
        else
        {
            if (other.CompareTag("Player"))
            {
                if (other.GetComponent<JumpMovement>() != null)
                {
                    other.gameObject.GetComponent<JumpMovement>().health = 0;
                }
            }
            else
            {
                Destroy(other.gameObject);
            }  
        }

    }

    void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.CompareTag("Don't Destroy"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<JumpMovement>() != null)
                {
                    collision.gameObject.GetComponent<JumpMovement>().health = 0;
                }
            }
            else if (collision.gameObject.transform.parent != null)
            {
                if (collision.gameObject.transform.parent.CompareTag("player"))
                {
                    if (collision.gameObject.transform.parent.GetComponent<JumpMovement>() != null)
                    {
                        collision.gameObject.transform.parent.GetComponent<JumpMovement>().health = 0;
                    }
                }
                else
                {
                    Destroy(collision.gameObject.transform.parent.gameObject);
                }
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
