using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	[SerializeField]
	private float z_Axe_Movement = 1f;
	[SerializeField]
	private float speed = 1f;
	private Rigidbody rigidbody;
	
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		float moveHorizontal = Input.GetAxis("Horizontal");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, z_Axe_Movement);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3(rigidbody.position.x + movement.x, 0, rigidbody.position.z + movement.z);

	}
}
