﻿using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {

	public float enginePower = 50.0f;
	public float maxSteer = 10.0f;

	public WheelCollider wheelFL;
	public WheelCollider wheelFR;
	public WheelCollider wheelRL;
	public WheelCollider wheelRR;
	
	// Update is called once per frame
	void FixedUpdate () {
		wheelRR.motorTorque = enginePower * Input.GetAxis ("Vertical");
		wheelRL.motorTorque = enginePower * Input.GetAxis ("Vertical");
		wheelFL.steerAngle = maxSteer * Input.GetAxis ("Horizontal");
		wheelFL.steerAngle = maxSteer * Input.GetAxis ("Horizontal");
	
	}
}
