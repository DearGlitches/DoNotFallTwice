using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using SimpleFirebaseUnity;
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

}
