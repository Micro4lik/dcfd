using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //для отображения размера лабиринта в инспекторе
public struct IntVector2 {

	public int x, z;

	public IntVector2 (int x, int z) { //создание вектора с целыми числами для добавления координат в MazeCell
		this.x = x;
		this.z = z;
	}

	public static IntVector2 operator + (IntVector2 a, IntVector2 b) { //поддержка оператора "+"
		a.x += b.x;
		a.z += b.z;
		return a;
	}
}
