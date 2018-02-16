using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;



public class GameVars : MonoBehaviour
{
	public Camera motion_blur_camera;
	public PostProcessingProfile notdrunk;
	public PostProcessingProfile littledrunk;
	public PostProcessingProfile drunk;
	
	
	private int health = 2;
	public float maxAlcool = 1f;
	public float alcoolLossPerTime = 0.005f;
	public float alcool;
	public float drinkDelta = 0.5f;	// time you can drink per time
	public float alcoolLossDelta = 0.1f; // delta for alcohol loss
	public float alcoolPerDrink = 0.1f;	// alcohol per drink
	private float myDrinkTime = 0.0f;	// used to check if we can drink again
	private float myAlcoolLossTime = 0.0f; // used to check if we can lose alcool again
	private float nextDrink = 0.5f;	// used to store the next minimum time for a drink
	private float nextLoss = 0.1f;	// used to store the next minimum time for an alcool loss
	public bool GameEnded { get; private set; }
	public float score = 0f;
	public float difficulty = 1f;	// game difficulty, used to change car speed

	public AudioClip[] collisonSounds;
	private AudioSource audioSrc;

	
	// Use this for initialization
	void Start ()
	{
		audioSrc = (AudioSource)gameObject.GetComponent<AudioSource>();
		//alcool = 0f;
		GameEnded = false;
	}
	
	// Update is called once per frame
	void Update ()
	{

		myDrinkTime += Time.deltaTime;
		myAlcoolLossTime += Time.deltaTime;

		if (Input.GetButton("Jump") && myDrinkTime > nextDrink && alcool < maxAlcool)
		{
			nextDrink = myDrinkTime + drinkDelta;
			alcool += alcoolPerDrink;

			nextDrink -= myDrinkTime;
			myDrinkTime = 0.0f;
		}

		if (myAlcoolLossTime > nextLoss)
		{
			nextLoss = myAlcoolLossTime + alcoolLossDelta;
			alcool -= alcoolLossPerTime;

			nextLoss -= myAlcoolLossTime;
			myAlcoolLossTime = 0.0f;


			score += alcool > 0 ? alcool : 0;
		}

		if (alcool <= 0)
		{
			alcool = 0;
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = notdrunk;
		}else if (alcool > 0.4f)
		{
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = littledrunk;
		}
		else if (alcool > 0.7f)
		{
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = drunk;
		}

		
		
		

	}

	public void fall()
	{
		AudioClip clip = collisonSounds[Random.Range(0, collisonSounds.Length - 1)];
		float vol = Random.Range(0.2f, 0.5f);
		audioSrc.PlayOneShot(clip, vol);
		
		if (--health == 0)
		{
			endGame();
		}
	}

	private void endGame()
	{
		Debug.Log("gameOver");
		Debug.Log("Score: " + score);
		Time.timeScale = 0;
		GameEnded = true;
	}

	public void nextLevel()
	{
		Debug.Log("Next Level");
		Debug.Log("Score: " + score);
		Time.timeScale = 0;
		GameEnded = true;
	}


}
