using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class heartCounter : MonoBehaviour {
	public GameObject heart;
	public ZombieAnimator lives;
	public Camera mainCamera;
	private List<Vector3> cood = new List<Vector3>();
	private List<GameObject> temp = new List<GameObject>();
	// Use this for initialization
	void Start () {
		for (int i = -1; i < 2; i++) {
			cood.Add(new Vector3((i * 5 / transform.position.x), 0));
			Debug.Log(transform.position.x + " " + transform.position.y);
		}
		for (int i = 0; i < cood.Count; i++) {
			temp.Add (Instantiate (heart, cood [i], Quaternion.identity) as GameObject);
			Vector3 tempT = temp[i].transform.position + transform.position;
			temp[i].transform.position = tempT;
			temp[i].transform.SetParent(transform.parent);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (lives.returnLives() < temp.Count)
		{
			int livesLeft = lives.returnLives();
			Destroy(temp[livesLeft]);
			temp.RemoveAt(livesLeft);
		}
	}
}
