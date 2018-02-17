using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameVars : MonoBehaviour
{
	public Camera motion_blur_camera;
	public PostProcessingProfile notdrunk;
	public PostProcessingProfile littledrunk;
	public PostProcessingProfile drunk;

	public Text drunk_info;
	private Color drunk_info_color;
	private int info_text_selector = 0;
	private string[] drunk_info_text;

	public Text levelIndicator;
	
	public int health = 1;
	public float maxAlcool = 1f;
	public float alcoolLossPerTime = 0.005f;
	public float alcool;
	public float drinkDelta = 0.5f;	// time you can drink per time
	public float alcoolLossDelta = 0.1f; // delta for alcohol loss
	public float alcoolPerDrink = 0.1f;	// alcohol per drink
	private float myDrinkTime = 0.0f;	// used to check if we can drink again
	private float myAlcoolLossTime = 0.0f; // used to check if we can lose alcool again
	private float nextDrink = 0.5f;	// used to store the next minimum time for a drink
	private float nextLoss = 0.1f;	// used to store the next minimum time for an alcool loss
	public bool GameEnded { get; private set; }
	public float slowWalkScoreMultiplier = 1f;

	// vars dependant of level don't set them
	public int level;
	public float score;
	public float difficulty;	// game difficulty, used to change car speed

	public AudioClip[] collisonSounds;
	private AudioSource audioSrc;

	public AudioClip deathSound;
	public AudioClip collisonWithCarSound;

	public AudioClip drinkingSound;

	public Animator simon;

	public Texture BlackoutTexture;
	
	public bool isInvicible = false;
	private float invicibleEndTime = 0f;
	private bool isFalling = false;

	private Color _previousGUIColor;

	
	// Use this for initialization
	void Start ()
	{
		
		audioSrc = (AudioSource)gameObject.GetComponent<AudioSource>();
		_previousGUIColor = GUI.color;
		//alcool = 0f;
		GameEnded = false;

		score = PlayerPrefs.GetFloat("score");
		difficulty = PlayerPrefs.GetFloat("difficulty");
		level = PlayerPrefs.GetInt("level");
		Debug.Log(difficulty);

		drunk_info_text = new string[] {"MMH BEER!", ".. #! .. OUH ..", "Bit D d D D Drunken", "Does anyone have a beer?"};
		info_text_selector = 0;

		levelIndicator.text = "Level " + level;
		Destroy(levelIndicator, 3);
		
		
		
	}

	/*private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}
	*/

	// Update is called once per frame
	void Update ()
	{
		if (simon.GetBool("falling"))
		{
	/*		if (invicibleEndTime > Time.time)
			{
				isInvicible = false;
				simon.SetBool("invincible", false);
			}
*/
			alcool = 0;
			return;
		}


		myDrinkTime += Time.deltaTime;
		myAlcoolLossTime += Time.deltaTime;

		if (Input.GetButton("Jump") && myDrinkTime > nextDrink && alcool < maxAlcool)
		{
			audioSrc.PlayOneShot(drinkingSound, 1f);
			nextDrink = myDrinkTime + drinkDelta;
			alcool += alcoolPerDrink;

			nextDrink -= myDrinkTime;
			myDrinkTime = 0.0f;
		}

		if (myAlcoolLossTime > nextLoss)
		{
			nextLoss = myAlcoolLossTime + alcoolLossDelta;
			alcool -= alcoolLossPerTime;

			nextLoss -= myAlcoolLossTime;
			myAlcoolLossTime = 0.0f;


			score += alcool > 0 ? alcool*slowWalkScoreMultiplier : 0;
		}


		drunk_info_color = drunk_info.color;
		
		
		if (alcool < 0)
		{
			alcool = 0;
		}
		else if(alcool < 0.4f)
		{
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = notdrunk;
			drunk_info.text = "";
			Debug.Log(drunk_info_text.Length);
			info_text_selector = (int)(Random.value * (drunk_info_text.Length-1));
			Color.Lerp(drunk_info_color, Color.black, 2f);
		}
		else if (alcool < 0.7f)
		{
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = littledrunk;	
			drunk_info.text = drunk_info_text[info_text_selector];		
		}
		else
		{
			motion_blur_camera.GetComponent<PostProcessingBehaviour>().profile = drunk;
		}

	}

	private void OnGUI()
	{
		
		if (alcool > 0.65f)
		{
			_previousGUIColor = GUI.color;
			GUI.color = new Color(0.0f, 0.0f, 0.0f, alcool);
			GUI.DrawTexture(new Rect(0,0,10000,10000), BlackoutTexture, ScaleMode.ScaleToFit, true, 1.0f);
		}
		else
		{
			GUI.color = _previousGUIColor;
		}
	}

	// fucking self-explanotory var
	public void fall(bool carInvolvedInAccident)
	{
		if (simon.GetBool("invincible"))
		{
			return;
		}
		

		
		isFalling = true;

		
		
		if (--health == 0)
		{
			audioSrc.PlayOneShot(deathSound, 1f);
			endGame();
		}
		else
		{
			simon.SetBool("falling", true);
			float vol = Random.Range(0.4f, 1f);
			if (carInvolvedInAccident)
			{
				audioSrc.PlayOneShot(collisonWithCarSound, vol);
			}
			else
			{
				AudioClip clip = collisonSounds[Random.Range(0, collisonSounds.Length - 1)];
				audioSrc.PlayOneShot(clip, vol);
			}

		}
		isInvicible = true;
		StartCoroutine("fallTimeout");
		

	}

	IEnumerator invincibleTimout()
	{
		Debug.Log("invincible Timout");
		yield return new WaitForSeconds(1.5f);
		simon.SetBool("invincible", false);
	}

	IEnumerator fallTimeout()
	{
		Debug.Log("FallTimeout");
		yield return new WaitForSeconds(1f);
		simon.SetBool("invincible", true);
		simon.SetBool("falling", false);	
		StartCoroutine("invincibleTimout");
	}

	private void endGame()
	{
		Debug.Log("gameOver");
		Debug.Log("Score: " + score);
		StartCoroutine("deathTimeout");
		//Time.timeScale = 0;
		GameEnded = true;
	}

	IEnumerator deathTimeout()
	{
		Debug.Log("EndTimeout");
		simon.SetBool("isDead", true);
		yield return new WaitForSeconds(3f);
		PlayerPrefs.SetFloat("score", score);
		PlayerPrefs.SetFloat("difficulty", difficulty);
		SceneManager.LoadScene("GameOver");		
	}

	public void nextLevel()
	{
		Debug.Log("Next Level");
		Debug.Log("Score: " + score);
		// Time.timeScale = 0;
		GameEnded = true;
		PlayerPrefs.SetFloat("score", score);
		PlayerPrefs.SetFloat("difficulty", difficulty*1.2f);
		PlayerPrefs.SetInt("level", level+1);
		SceneManager.LoadScene("Main");
	}


}
