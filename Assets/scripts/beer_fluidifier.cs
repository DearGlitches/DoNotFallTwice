using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class beer_fluidifier : MonoBehaviour
{

	public Image image;
	private float x;
	private float y;
	private float r;
	
	// Use this for initialization
	void Start ()
	{
		x = image.rectTransform.rect.x;
		y = image.rectTransform.rect.y;
		StartCoroutine( fluidifier() );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private IEnumerator fluidifier()
	{

		// process pre-yield

		r = (Random.value + 1) - 1;
		
		image.rectTransform.localPosition = new Vector3(x+r,y);
		
		yield return new WaitForSeconds( 1.0f );
		// process post-yield
		StartCoroutine( fluidifier() );
	}
}
