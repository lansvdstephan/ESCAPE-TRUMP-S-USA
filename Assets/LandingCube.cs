using UnityEngine;
using System.Collections;

public class LandingCube : MonoBehaviour {

    private Animator anim;
    private int animLandingHash = Animator.StringToHash("On Ground");

    void Start()
    {
        anim = this.GetComponentInParent<Animator>();
    }
	// Use this for initialization
    void OnTriggerStay(Collider other)
    {
        anim.SetBool(animLandingHash, true);
    }

    void OnTriggerExit(Collider other)
    {
        anim.SetBool(animLandingHash, false);
    }
}
