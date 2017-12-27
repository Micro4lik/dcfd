using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public int r1, r2, r3, r4;

	private void Start ()
	{
		StartCoroutine (BeginGame ());
	}

	public static int Q = 0;
	public static int test = 0;
	public static int impasseCount = 0;
	//public static int [,] impasseC = new int[10,10];
	public static int impasseRandom;
	public static int cor1, cor2;
	public static List<int[]> qwe = new List<int[]> ();

	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			LevelCount = 0;
			GameManager.MazeSize = 4;
			impasseCount = 0;
			cor1 = 99;
			cor2 = 99;
			qwe.Clear ();
			//ChestDummy.Clear ();
			RestartGame ();
		}
		if (Q == 1) {
			Q = 0;
			impasseCount = 0;
			cor1 = 99;
			cor2 = 99;
			qwe.Clear ();
			//ChestDummy.Clear ();
			RestartGame ();
		
		}
	}

	public Player playerPrefab;

	public GameObject StartPrefab;
	private GameObject StartDummy;
	public GameObject FinishPrefab;
	private GameObject FinishDummy;

	public GameObject ChestPrefab;
	public static List<GameObject> ChestDummy = new List<GameObject> ();


	public int StartCorner;
	public int FinishCorner;

	public GameObject InfoPanel;
	public Text LevelNumber;
	public static int LevelCount = 0;
	public static int MazeSize = 4;

	private Player playerInstance;

	public Maze mazePrefab;

	private Maze mazeInstance;

	public void GenerateStartFinish ()
	{

		StartCorner = Random.Range (0, 4);
		FinishCorner = Random.Range (0, 4);
		while (StartCorner == FinishCorner) {
			FinishCorner = Random.Range (0, 4);
		}
			
		switch (StartCorner) {
		case 0:
			r1 = Random.Range (0, 2);
			r2 = Random.Range (0, 2);
			break;
		case 1:
			r1 = Random.Range (0, 2);
			r2 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			break;
		case 2:
			r1 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			r2 = Random.Range (0, 2);
			break;
		case 3:
			r1 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			r2 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			break; 
		}
		switch (FinishCorner) {
		case 0:
			r3 = Random.Range (0, 2);
			r4 = Random.Range (0, 2);
			break;
		case 1:
			r3 = Random.Range (0, 2);
			r4 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			break;
		case 2:
			r3 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			r4 = Random.Range (0, 2);
			break;
		case 3:
			r3 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			r4 = Random.Range (mazeInstance.cells.GetUpperBound (0) - 1, mazeInstance.cells.GetUpperBound (0) + 1);
			break; 
		}

			
	}


	private IEnumerator BeginGame ()
	{		
		mazeInstance = Instantiate (mazePrefab) as Maze;
		yield return StartCoroutine (mazeInstance.Generate ());
		playerInstance = Instantiate (playerPrefab) as Player;

		LevelNumber.text = "Level " + LevelCount;

		for (int i = 0; i < GameManager.MazeSize; i++) {

			for (int q = 0; q < GameManager.MazeSize; q++) {

				test = 0;

				foreach (Transform child in mazeInstance.cells[i, q].transform) {

					if (child.name == "MazePassage(Clone)") {
						test += 1;
					}
					if (child.name == "MazeDoor1(Clone)") {
						test -= 3;
					}
							
				}

				if (test == 1) {

					int[] inp = { i, q };

					qwe.Add (inp);

					foreach (var item in qwe[impasseCount]) {							
						Debug.Log (item);
					}

					Debug.Log (impasseCount);
					impasseCount += 1;

					Debug.Log ("Это тупик!");
					Debug.Log (mazeInstance.cells [i, q]);
				}

			}

		}

		GenerateStartFinish1 ();

		Debug.Log ("Number of random impasse: " + StartCorner);

		int[] massiv = qwe [StartCorner];

		cor1 = massiv [0];
		cor2 = massiv [1];

		Debug.Log ("1: " + cor1);
		Debug.Log ("2: " + cor2);

		playerInstance.SetLocation (mazeInstance.cells [cor1, cor2]);

		StartDummy = Instantiate (StartPrefab, playerInstance.transform.position, Quaternion.identity);

		int[] massiv1 = qwe [FinishCorner];

		cor1 = massiv1 [0];
		cor2 = massiv1 [1];

		FinishDummy = Instantiate (FinishPrefab, mazeInstance.cells [cor1, cor2].transform.position, Quaternion.identity);

		CreateChests ();

	}

	public void GenerateStartFinish1 ()
	{

		StartCorner = Random.Range (0, qwe.Count);
		FinishCorner = Random.Range (0, qwe.Count);
		while (StartCorner == FinishCorner) {
			FinishCorner = Random.Range (0, qwe.Count);
		}
			
	}

	public void CreateChests ()
	{

		Debug.Log ("Try create chest" + qwe.Count);

		for (int i = 0; i < qwe.Count; i++) {

			if ((i == StartCorner) || (i == FinishCorner)) {
				Debug.Log ("Nedostatochto tupikov.");
				Debug.Log ("i = " + i + " SC = " + StartCorner + " FC = " + FinishCorner);
				//break;
				continue;
			}

			Debug.Log ("Vishli za cikl");

			int[] impasse = qwe [i];

			int c1 = impasse [0];
			int c2 = impasse [1];

			ChestDummy.Add (Instantiate (ChestPrefab, mazeInstance.cells [c1, c2].transform.position, Quaternion.identity)); //массив сундуков, чтобы дестроить сразу несколько

		}


	}

	public void RestartGame ()
	{
		StopAllCoroutines (); //останавливаем запущенную корутину, чтобы не возникало ошибок при генерации лабиринта при уже запущенной корутине
		Destroy (mazeInstance.gameObject);

		if (StartDummy != null) {
			Destroy (StartDummy.gameObject);
		}

		if (FinishDummy != null) {
			Destroy (FinishDummy.gameObject);
		}

		/*if (ChestDummy != null) {
			Destroy (ChestDummy.gameObject);
		}*/

		if (ChestDummy != null) {
			foreach (var chest in ChestDummy) {	
				Destroy (chest);
			}

			ChestDummy.Clear ();
		}


		if (playerInstance != null) {
			Destroy (playerInstance.gameObject);
		}
						
		StartCoroutine (BeginGame ());
	}
}