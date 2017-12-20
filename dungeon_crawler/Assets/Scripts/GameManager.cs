using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int r1, r2, r3, r4;

	private void Start () {
		StartCoroutine(BeginGame());
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
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
		
	private IEnumerator BeginGame () {		
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		//playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		GenerateStartFinish ();

		playerInstance.SetLocation(mazeInstance.cells[r1,r2]);
		StartDummy = Instantiate (StartPrefab, playerInstance.transform.position, Quaternion.identity);
		FinishDummy = Instantiate (FinishPrefab, mazeInstance.cells[r3,r4].transform.position, Quaternion.identity);
	}
		
	private void RestartGame () {
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