using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

	public GameObject CarPrefab;
	
	[Range(0,10)]
	public int NumberOfCars;

	[Range(0.0f,1.0f)]
	public float BaseSpeed;

	public Vector3 TopStart;
	public Vector3 BottomStart;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
