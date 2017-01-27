using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumpThrowingFunction : MonoBehaviour
{
    public Movement mv;
	public void Shoot()
    {
        if(mv.points <2650f)
		transform.parent.GetComponent<TTMovement> ().ShootNew ();
	}
}
