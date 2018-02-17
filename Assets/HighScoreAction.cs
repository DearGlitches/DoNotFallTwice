using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HighScoreAction : MonoBehaviour {

	public Button menu;
	public Text score1;
	public Text score2;
	public Text score3;
	
	// Use this for initialization
	void Start () {
		menu.onClick.AddListener(MenuGame);
	}
	
	void MenuGame()
	{
		SceneManager.LoadScene("Menu");
	}
	
	void Update ()
	{
		score1.text = "Olivier Pedophilo";
		score2.text = "Owen the Oven";
		score3.text = "Leo l'ours";
	}

}
