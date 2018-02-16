using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

	public Text score;
	public Button restartBtn;
	public Button menuBtn;

	// Use this for initialization
	void Start ()
	{
		this.score.text = "Score: " + (int)PlayerPrefs.GetFloat("score");
		restartBtn.onClick.AddListener(restart);
		menuBtn.onClick.AddListener(menu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void restart()
	{
		SceneManager.LoadScene("Main");
	}

	void menu()
	{
		SceneManager.LoadScene("Menu");
	}
}
