using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
    public static DontDestroyOnLoad instance = null;
    // Use this for initialization
    void Awake()
    {
         DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(this.GetType()).Length>1){
            Destroy(this.gameObject);
        }
    }
}
