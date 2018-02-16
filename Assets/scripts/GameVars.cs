using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars : MonoBehaviour
{
	private int health = 2;
	[SerializeField] private float maxAlcool = 1f;
	[SerializeField] private float alcoolLossPerFrame = 0.01f;
	public float alcool;
	[SerializeField] private float drinkDelta = 0.5f;	// time you can drink per time
	[SerializeField] private float alcoolPerDrink = 0.1f;	// alcohol per drink
	private float myDrinkTime = 0.0f;
	private float nextDrink = 0.5f;
	public bool gameEnded = false;
	

	
	// Use this for initialization
	void Start ()
	{
		//alcool = 0f;
	}
	
	// Update is called once per frame
	void Update ()
	{

		myDrinkTime += Time.deltaTime;

		if (Input.GetButton("Jump") && myDrinkTime > nextDrink && alcool < maxAlcool)
		{
			nextDrink = myDrinkTime + drinkDelta;
			alcool += alcoolPerDrink;

			nextDrink -= myDrinkTime;
			myDrinkTime = 0.0f;
		}

		alcool -= alcoolLossPerFrame;
		if (alcool < 0)
			alcool = 0;
		

	}

	public void fall()
	{
		if (--health == 0)
		{
			endGame();
		}
	}

	private void endGame()
	{
		Debug.Log("gameOver");
		Time.timeScale = 0;
		gameEnded = true;
	}

	public void nextLevel()
	{
		Debug.Log("Next Level");
		Time.timeScale = 0;
		gameEnded = true;
	}


}
