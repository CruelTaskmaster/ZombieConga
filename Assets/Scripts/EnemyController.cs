using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public enum ScreenEdge : int{LEFT, RIGHT, TOP, BOTTOM};
	public ScreenEdge choice;
	public float speed = 0;
	public ScreenRelativePosition spawnObject;

	private Transform spawnPoint;
	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnBecameInvisible()
	{
		spawnObject.setScreenEdge ((int)choice);
		spawnObject.reSpawn ();
		spawnPoint = GameObject.Find("SpawnPoint").transform;
		Debug.Log(spawnPoint.position.x + " " + spawnPoint.position.y);
		float yMax = Camera.main.orthographicSize - 1.2f;
		if (speed < 0) {
			transform.position = new Vector3 (spawnPoint.position.x, Random.Range (-yMax, yMax), transform.position.z);
		} else if (speed > 0) {
			transform.position = new Vector3 (spawnPoint.position.x, Random.Range (-yMax, yMax), transform.position.z);
		}
	}
}
