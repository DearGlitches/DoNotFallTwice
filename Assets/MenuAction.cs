using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAction : MonoBehaviour
{

	public Button start;
	public Button score;
	public Button exit;
	private float baseDifficulty = 0.7f; // should be change in GameOverController.cs as well 

	public Image title;

	public GameObject menu;
	
	// Use this for initialization
	void Start () {
		start.onClick.AddListener(StartGame);
		score.onClick.AddListener(HighScore);
		exit.onClick.AddListener(ExitGame);
		title.CrossFadeAlpha(0.0f, 3.5f, false);
		Destroy(title, 3.5f);
	}
	
	void StartGame()
	{
		PlayerPrefs.SetFloat("score", 0f);
		PlayerPrefs.SetFloat("difficulty", baseDifficulty);
		PlayerPrefs.SetInt("level",1);
		SceneManager.LoadScene("Main");
	}
	
	void HighScore()
	{
		SceneManager.LoadScene("Highscore");
	}
	
	void ExitGame()
	{
		Application.Quit();
	}
}
