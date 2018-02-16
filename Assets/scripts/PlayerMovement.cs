using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{

	public float z_Axe_Movement = 1f;
	public float speed = 1f;
	private Rigidbody rigidbody;
	public float xMin = -10f;
	public float xMax = 10f;
	private GameVars gameVars;
	public GameObject globalvars;
	public float endZDistance = 100f;
	
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		
		gameVars = globalvars.GetComponent<GameVars>();
		Debug.Log("testing console");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!gameVars.gameEnded)
		{
			// end of the road back home
			if (rigidbody.position.z >= endZDistance)
			{
				gameVars.nextLevel();
			}

			float moveHorizontal = Input.GetAxis("Horizontal");
		
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, z_Axe_Movement);
			rigidbody.velocity = movement * speed;
		
			rigidbody.position = new Vector3(
				Mathf.Clamp(rigidbody.position.x + movement.x, xMin, xMax),
				-10f,
				movement.z+rigidbody.position.z);
		}


	}
	
	// Use to check if Collison
	private void OnTriggerEnter(Collider other)
	{
		// Debug.Log("collision");
		if (other.CompareTag("Collision"))
		{
			gameVars.fall();
		}
	}
}
