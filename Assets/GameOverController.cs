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
	public float baseDifficulty = 0.8f;

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
		PlayerPrefs.SetInt("score", 0);
		PlayerPrefs.SetFloat("difficulty", baseDifficulty);
		SceneManager.LoadScene("Main");
	}

	void menu()
	{
		PlayerPrefs.SetInt("score", 0);
		PlayerPrefs.SetFloat("difficulty", baseDifficulty);
		SceneManager.LoadScene("Menu");
	}
}
