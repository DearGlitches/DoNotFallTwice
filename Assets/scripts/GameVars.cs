using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars : MonoBehaviour
{
	private int health = 2;
	[SerializeField] private float maxAlcool = 10f;
	[SerializeField] private float alcoolLossPerFrame = 0.01f;
	public float alcool;
	[SerializeField] private float drinkDelta = 0.5f;	// time you can drink per time
	[SerializeField] private float alcoolPerDrink = 0.3f;	// alcohol per drink
	private float myDrinkTime = 0.0f;
	private float nextDrink = 0.5f;
	

	
	// Use this for initialization
	void Start ()
	{
		alcool = 0f;
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

		maxAlcool -= alcoolLossPerFrame;

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
		Time.timeScale = 0;
	}
}
