using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;


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
	public int remainingDistance = 0;
	private float oldlRandomMove = 0f;
	public float drunkMoveMult = 0.7f;
	private float xRandomMin = -1f;	// min x random move for simon's walk
	private float xRandomMax = 1f;	// max x random move for simon's walk

	private static Random random = new Random();
	private static bool haveNextNextGaussian;
	private static double nextNextGaussian;
	private float sigma = 0.3f; // sigma for random's walk gaussian

	public Camera cameraPlayer; // used to sway camera while drunk
	public GameObject player; // used to sway while drunk
	public GameObject road; // used to sway while drunk
	public AudioSource audioSrc;
	public AudioClip[] passingCars;
	
	public Animator simon;
	
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody>();
		
		gameVars = globalvars.GetComponent<GameVars>();
		Debug.Log("testing console");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (simon.GetBool("falling"))
		{
			rigidbody.velocity = new Vector3(0,0,0);
		}
		else if (!gameVars.GameEnded)
		{
			// end of the road back home
			if (rigidbody.position.z >= endZDistance)
			{
				gameVars.nextLevel();
			}

			remainingDistance = (int)(endZDistance - rigidbody.position.z);

			// float moveHorizontal = Input.GetAxis("Horizontal") + (float)(Random.value - 0.5d)  * gameVars.alcool * drunkMoveMult;
			float randomMove = gaussianRandom(sigma, oldlRandomMove);
			oldlRandomMove = randomMove;
			float moveHorizontal = Input.GetAxis("Horizontal") + randomMove  * gameVars.alcool * drunkMoveMult;
			// oldlMove = Mathf.Clamp(moveHorizontal, xMin, xMax);
			
			/*
			 * Rotation, sway, drunk effect
			 */
			Quaternion targetCamera = Quaternion.Euler(0, 0, 0);
			Quaternion targetPlayer = Quaternion.Euler(90, -180, -180);
			Quaternion targetRoad = Quaternion.Euler(0, 0, 0);
			if(moveHorizontal < 0)
			{
				targetPlayer = Quaternion.Euler(90, -180, -180+10);
				targetCamera = Quaternion.Euler(0, -10* gameVars.alcool, 0);
				targetRoad = Quaternion.Euler(0, 0.5f, 0);
			}
			else if (moveHorizontal > 0)
			{
				targetPlayer = Quaternion.Euler(90, -180, -180-10);
				targetCamera = Quaternion.Euler(0, 10* gameVars.alcool, 0);
				targetRoad = Quaternion.Euler(0, -0.5f, 0);
			}

			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetPlayer, 0.1f);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetCamera, Time.deltaTime * 1f);
			road.transform.rotation = Quaternion.Slerp(road.transform.rotation, targetRoad, Time.deltaTime * 0.5f);
			
			//cameraPlayer.transform.localRotation.x = rotX * randomMove;
		
			Vector3 movement = new Vector3(moveHorizontal*Time.deltaTime, 0.0f, z_Axe_Movement*Time.deltaTime);
			rigidbody.velocity = movement * speed;
		
			rigidbody.position = new Vector3(
				Mathf.Clamp(rigidbody.position.x + movement.x*speed, xMin, xMax),
				-10f,
				rigidbody.position.z + movement.z* speed);
		}


	}

	private float gaussianRandom(float sigma, float offset)
	{
		float retVal;
		do
		{
			retVal = (float) NextGaussian() * sigma + offset;
		} while (retVal < xRandomMin || retVal > xRandomMax);

		return retVal;
	}
	
	private static double NextGaussian()
	{
		if (haveNextNextGaussian)
		{
			haveNextNextGaussian = false;
			return nextNextGaussian;
		}
		else
		{
			double v1, v2, s;
			do
			{
				v1 = 2 * Random.value - 1;
				v2 = 2 * Random.value - 1;
				s = v1 * v1 + v2 * v2;
			} while (s >= 1 || s == 0);
			double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
			nextNextGaussian = v2 * multiplier;
			haveNextNextGaussian = true;
			return v1 * multiplier;
		}
	}
	
	// Use to check if Collison
	private void OnTriggerEnter(Collider other)
	{
		// Debug.Log("collision");
		if (other.CompareTag("Collision"))
		{
			gameVars.fall(false);
		}
		else if (other.CompareTag("Car"))
		{
			gameVars.fall(true);
		} else if (other.CompareTag("SoundPassingCar"))
		{
			float vol = Random.Range(0.2f, 0.5f);
			AudioClip clip = passingCars[Random.Range(0, passingCars.Length - 1)];
			audioSrc.PlayOneShot(clip, vol);
		}
	}
}
