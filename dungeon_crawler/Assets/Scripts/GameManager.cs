using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private void Start () {
		StartCoroutine(BeginGame());
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	public Player playerPrefab;

	private Player playerInstance;

	public Maze mazePrefab;

	private Maze mazeInstance;

	private IEnumerator BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
	}

	/*private void BeginGame () {
		mazeInstance = Instantiate(mazePrefab) as Maze;
		//mazeInstance.Generate();
		StartCoroutine(mazeInstance.Generate());
	}*/

	/*private void RestartGame () {
		StopAllCoroutines(); //останавливаем запущенную корутину, чтобы не возникало ошибок при генерации лабиринта при уже запущенной корутине
		Destroy(mazeInstance.gameObject);
		BeginGame();
	}*/
	private void RestartGame () {
		StopAllCoroutines(); //останавливаем запущенную корутину, чтобы не возникало ошибок при генерации лабиринта при уже запущенной корутине
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}
}