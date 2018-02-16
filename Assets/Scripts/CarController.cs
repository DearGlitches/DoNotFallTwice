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

	public GameObject GlobalVars;

	private List<GameObject> _cars;
	
	
	// Use this for initialization
	void Start () {
		
		_cars = new List<GameObject>();

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

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
