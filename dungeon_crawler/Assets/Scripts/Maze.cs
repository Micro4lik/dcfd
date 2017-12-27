using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {


	public int GrowingTreeIndex;

	public IntVector2 size;

	public MazeCell cellPrefab;

	public MazeCell[,] cells;

	public float generationStepDelay; //задержка при создании либаринта, чтобы было интересно смотреть за генерацией


	public MazePassage passagePrefab;
	public MazeWall wallPrefab;


	public MazeCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) { 
		//int currentIndex = activeCells.Count - 1;
		int currentIndex = (activeCells.Count - 1)/GrowingTreeIndex; //Growing Tree algorithm (можно выбирать средний или случайный индекс для интересных эффектов)
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex); //натыкаемся на существующую клетку, и для продолжения генерации, надо удалить индекс из листа
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}


	public MazeDoor doorPrefab;

	[Range(0f, 1f)]
	public float doorProbability;

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
		MazePassage passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}
		
	private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate(wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}



	public IEnumerator Generate () {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		//WaitForSeconds delay = new WaitForSeconds(0);
		cells = new MazeCell[GameManager.MazeSize, GameManager.MazeSize];
		//cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			//Debug.Log ("123");
			DoNextGenerationStep(activeCells);
			//Debug.Log (cells[size.x, size.z]);
		}

		//Debug.Log (cells);

	}

	public IntVector2 RandomCoordinates {
		get {
			//return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
			return new IntVector2(Random.Range(0, GameManager.MazeSize), Random.Range(0, GameManager.MazeSize));
		}
	}

	public bool ContainsCoordinates (IntVector2 coordinate) {
		//return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
		return coordinate.x >= 0 && coordinate.x < GameManager.MazeSize && coordinate.z >= 0 && coordinate.z < GameManager.MazeSize;
	}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;

		newCell.transform.localPosition =
			new Vector3(coordinates.x - GameManager.MazeSize * 0.5f + 0.5f, 0f, coordinates.z - GameManager.MazeSize * 0.5f + 0.5f);
			//new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}
}