using UnityEngine;
using System.Collections;

public class ScreenRelativePosition : MonoBehaviour {
	public enum ScreenEdge {LEFT, RIGHT, TOP, BOTTOM};
	public GameObject sceneCamera;
	public float yOffset;
	public float xOffset;

	private ScreenEdge screenEdge;

	public void reSpawn()
	{
		// 1
		Vector3 newPosition = transform.position;
		Camera camera = Camera.main;
		
		// 2
		switch(screenEdge)
		{
			// 3
		case ScreenEdge.LEFT:
			newPosition.x = camera.aspect * camera.orthographicSize + xOffset + sceneCamera.transform.position.x;
			newPosition.y = yOffset;
			break;
			
			// 4
		case ScreenEdge.TOP:
			newPosition.y = camera.orthographicSize + yOffset + sceneCamera.transform.position.y;
			newPosition.x = xOffset;
			break;
			
		case ScreenEdge.BOTTOM:
			newPosition.y = -(camera.orthographicSize + yOffset) + sceneCamera.transform.position.y;
			newPosition.x = xOffset;
			break;
			
		case ScreenEdge.RIGHT:
			newPosition.y = yOffset;
			newPosition.x = -(camera.aspect * camera.orthographicSize + xOffset) + sceneCamera.transform.position.x;
			break;
		}
		// 5
		transform.position = newPosition;
	}

	public void setScreenEdge(int choice)
	{
		this.screenEdge = (ScreenEdge) choice;
	}
}
