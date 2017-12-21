using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpot : MonoBehaviour {

	//public GameManager NewLevel;

	void OnTriggerEnter(Collider col) {
		Debug.Log ("2222");
		//GameManager.
		GameManager.LevelCount += 1;
		GameManager.Q = 1;
		//NewLevel.
		//RestartGame ();
	}


}
