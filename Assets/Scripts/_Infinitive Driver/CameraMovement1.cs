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
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, player.transform.position.z + offset.z);
	}
}
