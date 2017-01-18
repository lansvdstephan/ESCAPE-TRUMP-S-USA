using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLastLevel : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Jumper level");
        }
    }

}
