using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Road : MonoBehaviour
{
	[Range(5,20)]
	public int NumberOfObstacles;
	private Stack<GameObject> _obstacles;
	private GameObject[,] _obstacleGrid;
	private Vector3 _roadPosition;

	public GameObject WallPrefab;
	public GameObject HolePrefab;
	public GameObject BananaPrefab;
	public GameObject CarPrefab;
	

	public Road()
	{
		_obstacleGrid = new GameObject[8,6];
		_obstacles = new Stack<GameObject>();
		ChooseRandomObstacles();
	}

	private void ChooseRandomObstacles()
	{
		var rnd = new Random();
		
		for (var i = 0; i < NumberOfObstacles; i++)
		{
			var num = rnd.Next(4);
			_obstacles.Push(RandomObstacle(num));
		}


		for (var i = 1; i < _obstacleGrid.GetLength(0) - 1; i+=2)
		{
			for (var j = 1; j < _obstacleGrid.GetLength(1) - 1; j+=2)
			{
				var prob = rnd.NextDouble();
				if (!(prob > 0.5)) continue;
				var obstacle = _obstacles.Count > 0 ? _obstacles.Pop() : Instantiate(WallPrefab);
				obstacle.transform.position = _roadPosition + new Vector3(i + 0.5f, 0, j + 0.5f);
				_obstacleGrid[i, j] = obstacle;
			}
		}
		
		_obstacles.Clear();
		
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
			case 4:
				return Instantiate(CarPrefab);
			default:
				return Instantiate(WallPrefab);
		}
	}

	// Use this for initialization
	void Start ()
	{
		_obstacleGrid = new GameObject[8,6];
		_obstacles = new Stack<GameObject>();
		_roadPosition = transform.position;
		ChooseRandomObstacles();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
