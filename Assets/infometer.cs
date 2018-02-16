using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infometer : MonoBehaviour {

	public Text score;
	public Text distance;
	
	public GameObject globalvars;
	
	
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.score.text = globalvars.GetComponent<GameVars>().score.ToString("F0");
		this.distance.text = "0"+" [m]";
	}
}
