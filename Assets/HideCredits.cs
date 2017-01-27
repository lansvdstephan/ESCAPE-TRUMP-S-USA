using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCredits : MonoBehaviour {

    public GameObject Credits;
	// Use this for initialization
    public void CreditsEnded()
    {
        Credits.SetActive(false);
    }
}
