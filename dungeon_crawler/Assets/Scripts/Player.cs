using UnityEngine;

public class Player : MonoBehaviour {

	private MazeCell currentCell;

	public void SetLocation (MazeCell cell) {
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			SetLocation(edge.otherCell);
		}
	}

	private MazeDirection currentDirection;

	private void Rotate (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			//Move(MazeDirection.North);
			Move (currentDirection);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			//Move(MazeDirection.East);
			//Rotate(currentDirection.GetNextCounterclockwise());
			//Move (currentDirection.GetNextClockwise ());
			Rotate(currentDirection.GetNextClockwise());
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			//Move(MazeDirection.South);
			Move (currentDirection.GetOpposite ());
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			Rotate(currentDirection.GetNextCounterclockwise());
			//Move (MazeDirection.West);
			//transform.rotation = Quaternion.Euler(10, 0, 0);
			//transform.child
			//Rotate(currentDirection.GetNextClockwise());
			//Move (currentDirection.GetNextCounterclockwise ());
		/*} else if (Input.GetKeyDown (KeyCode.Q)) {
			Rotate(currentDirection.GetNextCounterclockwise());
			//Look(currentDirection.GetNextCounterclockwise ());
		} else if (Input.GetKeyDown (KeyCode.E)) {
			Rotate(currentDirection.GetNextClockwise());
			//Look (currentDirection.GetNextClockwise ());
		}*/
	}

}
}