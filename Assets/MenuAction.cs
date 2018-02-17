using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAction : MonoBehaviour
{

	public Button start;
	public Button tutorial;
	public Button exit;
	private float baseDifficulty = 0.7f; // should be change in GameOverController.cs as well 

	public GameObject menu;
	
	// Use this for initialization
	void Start () {
		start.onClick.AddListener(StartGame);
		exit.onClick.AddListener(ExitGame);
	}
	
	void StartGame()
	{
		PlayerPrefs.SetFloat("score", 0f);
		PlayerPrefs.SetFloat("difficulty", baseDifficulty);
		PlayerPrefs.SetInt("level",1);
		SceneManager.LoadScene("Main");
	}
	
	void Tutorial()
	{
		Vector3 pos = menu.GetComponent<RectTransform>().localPosition;
		pos.x -= 100f;
		menu.GetComponent<RectTransform>().localPosition = new Vector3(-100,0,0);
		//Vector3 menuPlacement = this.menu.gameObject.GetComponent<RectTransform>().position;
		//menuPlacement = new Vector3(-50*Time.deltaTime, 0.0f, 0.0f);
	}
	
	void ExitGame()
	{
		Application.Quit();
	}
}
