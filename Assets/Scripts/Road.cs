using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Road : MonoBehaviour
{
	[Range(5,20)]
	public int NumberOfObstacles;

	[Range(4,20)]
	public int NumberOfColumns;
	
	[Range(4,20)]
	public int NumberOfRows;
	private GameObject[,] _obstacleGrid;
	private Vector3 _roadPosition;
	private float _columnWidth;
	private float _rowHeight;

	public GameObject WallPrefab;
	public GameObject HolePrefab;
	public GameObject BananaPrefab;
	public GameObject CarPrefab;
	public GameObject BinPrefab;
	public GameObject RedBarrierPrefab;
	public GameObject YellowBarrierPrefab;
	


	private void ChooseRandomObstacles()
	{
		var rnd = new Random();


		for (var i = 1; i < _obstacleGrid.GetLength(0) - 1; i+=2)
		{
			for (var j = 1; j < _obstacleGrid.GetLength(1) - 1; j+=2)
			{
				var prob = rnd.NextDouble();
				if (!(prob > 0.5)) continue;
				var obstacle = RandomObstacle(rnd.Next(7));
				obstacle.transform.position = _roadPosition + new Vector3(_columnWidth * i + 0.5f, _rowHeight * j + 0.5f, -0.5f);
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
			case 4:
				return Instantiate(BinPrefab);
			case 5:
				return Instantiate(RedBarrierPrefab);
			case 6:
				return Instantiate(YellowBarrierPrefab);
			default:
				return Instantiate(WallPrefab);
		}
	}

	// Use this for initialization
	void Start ()
	{
		_obstacleGrid = new GameObject[NumberOfColumns,NumberOfRows];
		var _sizeX = GetComponent<SpriteRenderer>().size.x;
		var _sizeY = GetComponent<SpriteRenderer>().size.y;
		var box = GetComponent<BoxCollider2D>();
		_sizeX = box.size.x;
		_sizeY = box.size.y;
		_columnWidth = _sizeX / NumberOfColumns;
		_rowHeight = _sizeY / NumberOfRows;
		_roadPosition = transform.position - new Vector3(_sizeX/2, _sizeY/2, 0);
		ChooseRandomObstacles();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("return"))
		{
			ChooseRandomObstacles();
		}
	}
}
