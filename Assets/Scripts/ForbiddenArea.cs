using UnityEngine;
using System.Collections;

public class ForbiddenArea : MonoBehaviour {

    [Header("Dialogue")]
    public string[] dontEnter;
    public string[] mayEnter;

    private GameObject headPlayer;
    public Sprite guardhead;

    // Use this for initialization
    void Start () {
        if (dontEnter.Length == 0)
        {
            dontEnter = new string[1];
            dontEnter[0] = "You can't enter.";
        }
        if (mayEnter.Length == 0)
        {
            mayEnter = new string[1];
            mayEnter[0] = "Please enter.";
        }

        headPlayer = PhilMovement.head;
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            if (headPlayer.transform.childCount > 1)
            {
                PhilDialogue.Instance.AddNewDialogue(this.mayEnter, guardhead);
                GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                PhilDialogue.Instance.AddNewDialogue(this.dontEnter, guardhead);
            }
        }
    }
}
