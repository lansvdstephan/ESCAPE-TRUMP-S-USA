using UnityEngine;
using System.Collections;

public class CameraMovement1 : MonoBehaviour {
    public GameObject player;

    private Vector3 offset;

	void Start () {
        offset = this.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(0,0, player.transform.position.z) + offset;
	}
}
