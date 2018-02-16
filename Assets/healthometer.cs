using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthometer : MonoBehaviour
{

	public Image health1;
	public Image health2;
	
	public GameObject globalvars;

	private int health = 2;
	
	// Use this for initialization
	void Start () {
		this.health = globalvars.GetComponent<GameVars>().health;
	}
	
	// Update is called once per frame
	void Update () {
		this.health = globalvars.GetComponent<GameVars>().health;
		
		Color white = Color.white;
		Color black = Color.black;
		black.a = 0.5f;
		
		if (health == 1)
		{
			this.health1.color = Color.white;
			this.health2.color = black;
		}else if (health == 0)
		{
			this.health1.color = black;
			this.health2.color = black;
		}
		else
		{
			this.health1.color = Color.white;
			this.health2.color = Color.white;
		}
	}

	void damage()
	{
		health = health - 1;
		if (health <= 0)
			health = 0;
		
		
	}

	void setHealth(int health)
	{
		this.health = health;
	}
}
