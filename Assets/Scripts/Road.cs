using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Road : MonoBehaviour
{
	[Range(1, 20)]
	public int NumberOfObstacles = 5;

	private const int _numberOfColumns = 4;

	[Range(1, 20)]
	public int NumberOfRows = 4;
	
	
	private List<GameObject> _obstacles;
	private GameObject[,] _obstacleGrid;
	private Vector3 _roadPosition;
	private float _columnWidth;
	private float _rowHeight;

	public GameObject BinPrefab;
	public GameObject RedBarrierPrefab;
	public GameObject YellowBarrierPrefab;
	public GameObject BrokenBottlePrefab;
	


	private void ChooseRandomObstacles()
	{
		var rnd = new Random();


		for (int i = 0; i < NumberOfObstacles; i++)
		{
			var obstacle = RandomObstacle(rnd.Next(5));
			var _row = rnd.Next(NumberOfRows);
			var _column = rnd.NextDouble() >= 0.5 ? 0 : _numberOfColumns - 1;
			while (_obstacleGrid[_column, _row] != null)
			{
				_row = rnd.Next(NumberOfRows);
				_column = rnd.NextDouble() >= 0.5 ? 0 : _numberOfColumns - 1;
			}

			obstacle.transform.localScale = transform.localScale;

			
			obstacle.transform.position = _roadPosition + new Vector3(_columnWidth * _column + _columnWidth/2, 2, _rowHeight * _row + _rowHeight/2);
			obstacle.transform.parent = transform;
			_obstacleGrid[_column, _row] = obstacle;

			_obstacles.Add(obstacle);
		}
		
		

		
	}

	private GameObject RandomObstacle(int num)
	{
		switch (num)
		{
			case 0:
				return Instantiate(BrokenBottlePrefab);
			case 1:
				return Instantiate(BinPrefab);
			case 2:
				return Instantiate(RedBarrierPrefab);
			case 3:
				return Instantiate(YellowBarrierPrefab);
			default:
				return Instantiate(RedBarrierPrefab);
		}
	}

	// Use this for initialization
	void Awake ()
	{
		_obstacles = new List<GameObject>();
		_obstacleGrid = new GameObject[_numberOfColumns, NumberOfRows];
		var _sizeX = GetComponent<SpriteRenderer>().size.x;
		var _sizeY = GetComponent<SpriteRenderer>().size.y;
		var box = GetComponent<BoxCollider>();
		_sizeX = box.size.x;
		_sizeY = box.size.y;
		_columnWidth = _sizeX / _numberOfColumns;
		_rowHeight = _sizeY / NumberOfRows;
		_roadPosition = transform.position - new Vector3(_sizeX/2, 0, _sizeY/2);
		ChooseRandomObstacles();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKey("return"))
		{
			DestroyObstacles();
			ChooseRandomObstacles();
		}*/
	}

	private void DestroyObstacles()
	{
		for (var i = 0; i < _obstacles.Count; i++)
		{
			Destroy(_obstacles.ToArray()[i]);
		}
	}
}
