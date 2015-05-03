using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieAnimator : MonoBehaviour {
	public float moveSpeed;
	public float turnSpeed;
	public AudioClip enemyContactSound;
	public AudioClip catContactSound;
	public ScoreTracker scoreTracker;
	private Vector3 moveDirection;
	private List<Transform> congaLine = new List<Transform>();
	private bool isInvincible = false;
	private float timeSpentInvincible;
	private int lives = 3;
	private int score;
	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;
	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		score++;
		Vector3 currentPosition = transform.position;
		// 2
		if( Input.GetButton("Fire1") ) {
			// 3
			Vector3 moveToward = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			// 4
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0; 
			moveDirection.Normalize();
		}
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		/*float xBoundary = 8f, yBoundary = 2.57f;
		if (currentPosition.x <= -xBoundary) {
			currentPosition.x = -xBoundary;
		}
		else if (currentPosition.x >= xBoundary) {
			currentPosition.x = xBoundary;
		}
		if (currentPosition.y <= -yBoundary)	{
			currentPosition.y = -yBoundary;
		}
		else if (currentPosition.y >= yBoundary)	{
			currentPosition.y = yBoundary;
		}*/
		transform.position = Vector3.Lerp( currentPosition, target, Time.deltaTime );
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.Euler( 0, 0, targetAngle ), turnSpeed * Time.deltaTime );
		EnforceBounds();
		if (isInvincible)
		{
			//2
			timeSpentInvincible += Time.deltaTime;
			
			//3
			if (timeSpentInvincible < 3f) {
				float remainder = timeSpentInvincible % .3f;
				GetComponent<Renderer>().enabled = remainder > .15f; 
			}
			//4
			else {
				GetComponent<Renderer>().enabled = true;
				isInvincible = false;
			}
		}
	}
	public void SetColliderForSprite( int spriteNum )
	{
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders[currentColliderIndex].enabled = true;
	}
	void OnTriggerEnter2D( Collider2D other )
	{
		if(other.CompareTag("Cat")) {
			score += 100;
			GetComponent<AudioSource>().PlayOneShot(catContactSound);
			Transform followTarget = congaLine.Count == 0 ? transform : congaLine[congaLine.Count-1];
			other.transform.parent.GetComponent<CatController>().JoinConga( followTarget, moveSpeed, turnSpeed );
			congaLine.Add( other.transform );
			if (congaLine.Count >= 5) {
				Debug.Log("You won!");
				scoreTracker.win();
			}
		}
		else if (!isInvincible && other.CompareTag("enemy")) {
			if (score >= 200)
				score -= 200;
			else
				score = 0;
			GetComponent<AudioSource>().PlayOneShot(enemyContactSound);
			isInvincible = true;
			timeSpentInvincible = 0;
			for( int i = 0; i < 2 && congaLine.Count > 0; i++ )
			{
				int lastIdx = congaLine.Count-1;
				Transform cat = congaLine[ lastIdx ];
				congaLine.RemoveAt(lastIdx);
				cat.parent.GetComponent<CatController>().ExitConga();
			}
			if (--lives <= 0) {
				Debug.Log("You lost!");
				Application.LoadLevel("Lost");
			}
		}

	}
	private void EnforceBounds()
	{
		// 1
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;

		// 2
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;

		float yMax = mainCamera.orthographicSize;
		
		// 3
		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			moveDirection.x = -moveDirection.x;
		}
		// TODO vertical bounds
		if ( newPosition.y < -yMax || newPosition.y > yMax ) {
			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
			moveDirection.y = -moveDirection.y;
		}
		// 4
		transform.position = newPosition;
	}

	public string returnScore()
	{
		return score.ToString();
	}

	public int returnLives()
	{
		return lives;
	}
}
