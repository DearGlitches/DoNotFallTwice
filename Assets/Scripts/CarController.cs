using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{

	public GameObject CarPrefab;
	
	[Range(5,15)]
	public int FrequencyOfCars;

	[Range(0.0f,1.0f)]
	public float BaseSpeed;

	public Vector3 TopStart;
	public Vector3 BottomStart;

	public GameObject GlobalVars;

	private List<GameObject> _cars;

	private float _nextCarTime;

	private const float EPSILON = 0.01f;
	
	
	// Use this for initialization
	void Start () {
		
		_cars = new List<GameObject>();

		LaunchCars();
		

	}
	
	// Update is called once per frame
	void Update () {

		if (Math.Abs(_nextCarTime - Time.time) < EPSILON)
		{
			LaunchCars();
		}
		
	}

	private void LaunchCars()
	{
		var bottomCar = Instantiate(CarPrefab);
		var topCar = Instantiate(CarPrefab);

		var script = bottomCar.GetComponent<Car>();
		script.MovingUp = true;
		script.Globalvars = GlobalVars;
		bottomCar.transform.position = BottomStart;

		script = topCar.GetComponent<Car>();
		script.MovingUp = false;
		script.Globalvars = GlobalVars;
		topCar.transform.position = TopStart;
		
		_cars.Add(bottomCar);
		_cars.Add(topCar);

		_nextCarTime = Time.time + Random.value + FrequencyOfCars;
	}
}
