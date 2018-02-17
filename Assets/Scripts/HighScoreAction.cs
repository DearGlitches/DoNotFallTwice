using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class HighScoreAction : MonoBehaviour {

	public Button menu;
	public Text score1;
	public Text score2;
	public Text score3;

	private DatabaseReference _reference;
	
	// Use this for initialization
	void Start () {
		menu.onClick.AddListener(MenuGame);
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://donotfalltwice.firebaseio.com/");
		_reference = FirebaseDatabase.DefaultInstance.GetReference("scores");
		_reference.OrderByChild("score").LimitToLast(3).ValueChanged += HandleValueChanged;
	}
	
	void MenuGame()
	{
		SceneManager.LoadScene("Menu");
	}
	
	void Update ()
	{
		
		//score1.text = "Olivier Pedophilo";
		//score2.text = "Owen the Oven";
		//score3.text = "Leo l'ours";
	}

	void HandleValueChanged(object sender, ValueChangedEventArgs args)
	{
		if (args.DatabaseError != null)
		{
			Debug.LogError(args.DatabaseError.Message);
			return;
		}

		if (args.Snapshot != null && args.Snapshot.ChildrenCount > 0)
		{
			var count = 1;
			var scores = args.Snapshot.Children.Reverse();
			Debug.Log(args.Snapshot.ChildrenCount.ToString());
			foreach (var snapshotChild in scores)
			{
				var score = snapshotChild.Child("score");
				var name = snapshotChild.Child("name");

				if (count == 1)
				{
					score1.text = name.Value + ": " + score.Value;
				}
				else if (count == 2)
				{
					score2.text = name.Value + ": " + score.Value;
				}
				else if (count == 3)
				{
					score3.text = name.Value + ": " + score.Value;
				}

				count++;
			}
		}
	}

}
