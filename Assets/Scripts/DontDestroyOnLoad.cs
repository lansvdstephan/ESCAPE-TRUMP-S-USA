using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {
    public static DontDestroyOnLoad instance = null;
    // Use this for initialization
    void Awake()
    {
         DontDestroyOnLoad(gameObject);
    }
}
