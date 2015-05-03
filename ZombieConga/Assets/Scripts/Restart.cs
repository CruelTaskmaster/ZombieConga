using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {
	public void restart()
	{
		Application.LoadLevel ("Start");
	}
}
