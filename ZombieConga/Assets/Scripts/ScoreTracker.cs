using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {
	public int currentLevel;
	public Text score;
	public Text level;
	public ZombieAnimator za;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		score.text = za.returnScore ();
		level.text = "level: " + currentLevel.ToString();
	}

	public void win()
	{
		if (currentLevel < 2)
			Application.LoadLevel ("Start2");
		else
			Application.LoadLevel ("Won");
	}
}
