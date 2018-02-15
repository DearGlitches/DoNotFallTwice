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
	[SerializeField] private float xMin = -10f;
	[SerializeField] private float xMax = 10f;
	private GameVars gameVars;
	
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		gameVars = GetComponent<GameVars>();
		Debug.Log("testing console");
	}
	
	// Update is called once per frame
	void Update ()
	{

		float moveHorizontal = Input.GetAxis("Horizontal");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, z_Axe_Movement);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x + movement.x, xMin, xMax),
			-10f,
			movement.z+rigidbody.position.z);

	}
	
	// Use to check if Collison
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("collision");
		if (other.CompareTag("Collision"))
		{
			gameVars.fall();
		}
	}
}
