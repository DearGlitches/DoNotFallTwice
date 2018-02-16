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
	private List<GameObject> _obstacles;
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


		for (int i = 0; i < NumberOfObstacles; i++)
		{
			if (rnd.NextDouble() >= 0.5)
			{
				var obstacle = RandomObstacle(rnd.Next(7));
				var _row = rnd.Next(NumberOfRows);
				var _column = rnd.NextDouble() >= 0.5 ? 0 : 3;
				obstacle.transform.position = _roadPosition + new Vector3(_columnWidth * _column + _columnWidth/2, _rowHeight * _row + _rowHeight/2, -0.5f);
				_obstacles.Add(obstacle);
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
		_obstacles = new List<GameObject>();
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
			DestroyObstacles();
			ChooseRandomObstacles();
		}
	}

	private void DestroyObstacles()
	{
		for (var i = 0; i < _obstacles.Count; i++)
		{
			Destroy(_obstacles.ToArray()[i]);
		}
	}
}
