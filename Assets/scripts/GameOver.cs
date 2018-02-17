using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	float timer = 0;
	private float finalScore;


	// Use this for initialization
	void Start () {
		finalScore = PlayerPrefs.GetInt("score");
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		//if it's time to attack and player in range
		if (timer >= 2)
		{
			Application.LoadLevel("Menu");
		}
	}
}