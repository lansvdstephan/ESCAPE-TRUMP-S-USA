using UnityEngine;
using System.Collections;

public class CarInteract : PhilInteractable {

	public override void Interact(GameObject player){

		player.SetActive(false);
		//player.GetComponent<PhilMovement> ().enabled = false;
		//player.GetComponent<MeshRenderer> ().enabled = false;
		player.transform.parent = this.transform.FindChild ("Seat").transform;
		//player.transform.position = this.transform.FindChild ("Seat").transform.position;
		//player.GetComponent<BoxCollider> ().enabled = false;
		//player.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<CarControl> ().enabled = true;
	}

}
