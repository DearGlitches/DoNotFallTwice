﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

	public bool MovingUp;
	public float Speed;
	private float _realSpeed;

	private GameVars _gameVars;
	public GameObject Globalvars;
	
	// Use this for initialization
	void Start ()
	{
		_gameVars = Globalvars.GetComponent<GameVars>();
		_realSpeed = Speed * _gameVars.difficulty;
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += MovingUp ? Vector3.up * _realSpeed : Vector3.down * _realSpeed;

	}
}
