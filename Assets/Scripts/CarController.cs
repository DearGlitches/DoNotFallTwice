using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{

	public GameObject CarPrefab1;
	public GameObject CarPrefab2;
	
	[Range(5,15)]
	public int FrequencyOfCars = 10;

	[Range(0.0f,45.0f)]
	public float BaseSpeed = 0.5f;

	public Vector3 TopStart = new Vector3(-8f, -9.5f, 1150f);
	public Vector3 BottomStart = new Vector3(8f, -9.5f, -63f);

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
	void FixedUpdate () {

		if (Math.Abs(_nextCarTime - Time.time) < EPSILON)
		{
			LaunchCars();
		}
		
	}

	private void LaunchCars()
	{
		var bottomCar = Instantiate(CarPrefab1);
		if (Random.value >= 0.5f)
		{
			bottomCar = Instantiate(CarPrefab2);
		}

		var topCar = Instantiate(CarPrefab1);
		if (Random.value >= 0.5f)
		{
			topCar = Instantiate(CarPrefab2);
		}

		var script = bottomCar.GetComponent<Car>();
		script.MovingUp = true;
		script.Globalvars = GlobalVars;
		script.Speed = BaseSpeed;// + Random.value / 2;
		bottomCar.transform.position = BottomStart;
		bottomCar.transform.parent = transform;

		script = topCar.GetComponent<Car>();
		script.MovingUp = false;
		script.Globalvars = GlobalVars;
		script.Speed = BaseSpeed;// + Random.value / 2;
		topCar.transform.position = TopStart;
		topCar.transform.parent = transform;
		topCar.transform.rotation = Quaternion.Euler(90, 0, 180);
		
		_cars.Add(bottomCar);
		_cars.Add(topCar);

		_nextCarTime = Time.time + (Random.value+0.1f) * FrequencyOfCars;
	}
}
