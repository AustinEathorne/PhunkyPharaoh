using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmTest : MonoBehaviour {


	private bool isRed = false;

	void Update () 
	{
		if (AudioManager.isOnBeat) 
		{
			if (isRed)
				this.GetComponent<SpriteRenderer> ().color = Color.white;
			else
				this.GetComponent<SpriteRenderer> ().color = Color.red;

			isRed = !isRed;
			Debug.Log ("On Beat");
		}
	}
}
