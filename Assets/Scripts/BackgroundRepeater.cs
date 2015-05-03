using UnityEngine;
using System.Collections;

public class BackgroundRepeater : MonoBehaviour {
	private Transform cameraTransform;
	private float spriteWidth;
	// Use this for initialization
	void Start () {
		//1
		cameraTransform = Camera.main.transform;
		//2
		SpriteRenderer spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
		spriteWidth = spriteRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if( (transform.position.x + spriteWidth * 1.6f) < cameraTransform.position.x) {
			Vector3 newPos = transform.position;
			newPos.x += 3.2f * spriteWidth; 
			transform.position = newPos;
		}
	}
}
