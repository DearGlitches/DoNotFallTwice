using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GameOverController : MonoBehaviour
{

	public Text score;
	public Button restartBtn;
	public Button menuBtn;
	public Button SubmitButton;
	public InputField NameField;

	private DatabaseReference _reference;

	// Use this for initialization
	void Start ()
	{
		this.score.text = "Score: " + (int)PlayerPrefs.GetFloat("score");
		restartBtn.onClick.AddListener(restart);
		menuBtn.onClick.AddListener(menu);
		SubmitButton.onClick.AddListener(Submit);

		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://donotfalltwice.firebaseio.com");
		_reference = FirebaseDatabase.DefaultInstance.GetReference("scores");
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

	void Submit()
	{

		var name = NameField.text;
		var score = (int)PlayerPrefs.GetFloat("score");

		var json = "{\"name\":\"" + name + "\",\"score\":" + score + "}";
		Debug.Log(json);


		var key = _reference.Push().Key;

		_reference.Child(key).SetRawJsonValueAsync(json);

	}

}
