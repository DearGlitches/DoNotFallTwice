using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

	public bool MovingUp = true;
	public float Speed = 0.5f;
	
	// Use this for initialization
	void Start ()
	{	

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.position += MovingUp ? Vector3.forward * Speed : Vector3.back * Speed;

	}
}
