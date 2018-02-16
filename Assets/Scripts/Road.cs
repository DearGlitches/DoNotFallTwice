using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Road : MonoBehaviour
{
	[Range(5,20)]
	public int NumberOfObstacles;
	private GameObject[,] _obstacleGrid;
	private Vector3 _roadPosition;

	public GameObject WallPrefab;
	public GameObject HolePrefab;
	public GameObject BananaPrefab;
	public GameObject CarPrefab;

	private void ChooseRandomObstacles()
	{
		var rnd = new Random();


		for (var i = 1; i < _obstacleGrid.GetLength(0) - 1; i+=2)
		{
			for (var j = 1; j < _obstacleGrid.GetLength(1) - 1; j+=2)
			{
				var prob = rnd.NextDouble();
				if (!(prob > 0.5)) continue;
				var obstacle = RandomObstacle(rnd.Next(4));
				obstacle.transform.position = _roadPosition + new Vector3(i + 0.5f, j + 0.5f, -0.5f);
				_obstacleGrid[i, j] = obstacle;
			}
		}

		
	}

	private GameObject RandomObstacle(int num)
	{
		switch (num)
		{
			case 0:
				return Instantiate(WallPrefab);
			case 1:
				return Instantiate(HolePrefab);
			case 2:
				return Instantiate(BananaPrefab);
			case 3:
				return Instantiate(CarPrefab);
			default:
				return Instantiate(WallPrefab);
		}
	}

	// Use this for initialization
	void Start ()
	{
		_obstacleGrid = new GameObject[8,6];
		var _sizeX = GetComponent<SpriteRenderer>().size.x;
		var _sizeY = GetComponent<SpriteRenderer>().size.y;
		_roadPosition = transform.position - new Vector3(_sizeX/2, _sizeY/2, 0);
		ChooseRandomObstacles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
