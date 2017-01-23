using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpThrowingFunction : MonoBehaviour {

	public void Shoot(){
		transform.parent.GetComponent<TTMovement> ().ShootNew ();
		}
}
