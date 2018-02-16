using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class beerometer : MonoBehaviour {

	public Image image;
	public GameObject globalvars;
	
	private Resolution res;
	
	// inital beer-o-meter canvas size
	private float inital_beer_height 	= 0;
	private float inital_beer_width 	= 0;
	
	// Use this for initialization
	void Start ()
	{
		inital_beer_height 	= 	image.rectTransform.sizeDelta.y;
		inital_beer_width	=	image.rectTransform.sizeDelta.x;
		res = Screen.currentResolution;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (res.height != Screen.currentResolution.height || res.width != Screen.currentResolution.width)
		{
			res = Screen.currentResolution;
		}

		float v = globalvars.GetComponent<GameVars>().alcool;
		image.rectTransform.sizeDelta = new Vector2(inital_beer_width,v*inital_beer_height);
		
	}
}
