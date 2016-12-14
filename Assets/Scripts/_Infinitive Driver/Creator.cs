using UnityEngine;
using System.Collections;

public class Creator : MonoBehaviour {

    public GameObject[] obj;

    private int counter = 0;

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plane"))
        {
            if (counter % 2 == 0)
            {
                print("u smell");
                Instantiate(obj[Random.Range(0, obj.Length)], new Vector3(0, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                counter++;
            }
            else
            {
                Instantiate(obj[Random.Range(0, obj.Length)], new Vector3(0, this.gameObject.transform.position.y + 0.01f, this.gameObject.transform.position.z), Quaternion.identity);
                counter++;
            }
        }
    }
}
