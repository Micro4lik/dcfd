using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSpot : MonoBehaviour {

	//public GameManager NewLevel;

	void OnTriggerEnter(Collider col) {
		GameManager.LevelCount += 1;
		if (GameManager.MazeSize != 10)
		GameManager.MazeSize += 1;
		GameManager.Q = 1;
	}


}
