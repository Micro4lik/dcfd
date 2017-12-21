using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int r1, r2, r3, r4;

	private void Start () {
		StartCoroutine(BeginGame());
	}

	public static int Q = 0;

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			LevelCount = 0;
			/*if (Q == 0) {
				Q = 1;
				StartCoroutine(BeginGame());

			} else*/
			RestartGame();
		}
		if (Q == 1) {
			Q = 0;
			RestartGame();
		
		}
	}

	public Player playerPrefab;

	public GameObject StartPrefab;
	private GameObject StartDummy;
	public GameObject FinishPrefab;
	private GameObject FinishDummy;


	public int StartCorner;
	public int FinishCorner;

	public GameObject InfoPanel;
	public Text LevelNumber;
	public static int LevelCount = 0;

	private Player playerInstance;

	public Maze mazePrefab;

	private Maze mazeInstance;

	public void GenerateStartFinish () {

		StartCorner = Random.Range (0, 4);
		FinishCorner = Random.Range (0, 4);
		while (StartCorner == FinishCorner)
		{FinishCorner = Random.Range (0, 4);}
			
		switch (StartCorner)
		{
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
		switch (FinishCorner)
		{
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
		

	/*public void NewLevel () {
		StartCoroutine(BeginGame());
	}*/

	private IEnumerator BeginGame () {		
		Debug.Log ("123");
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		//playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		GenerateStartFinish ();

		LevelNumber.text = "Level " + LevelCount;

		playerInstance.SetLocation(mazeInstance.cells[r1,r2]);
		StartDummy = Instantiate (StartPrefab, playerInstance.transform.position, Quaternion.identity);
		FinishDummy = Instantiate (FinishPrefab, mazeInstance.cells[r3,r4].transform.position, Quaternion.identity);
	}
		
	public void RestartGame () {
		StopAllCoroutines(); //останавливаем запущенную корутину, чтобы не возникало ошибок при генерации лабиринта при уже запущенной корутине
		Destroy(mazeInstance.gameObject);

		if (StartDummy != null) {
			Destroy(StartDummy.gameObject);
		}

		if (FinishDummy != null) {
			Destroy(FinishDummy.gameObject);
		}

		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
			
		StartCoroutine(BeginGame());
	}
}