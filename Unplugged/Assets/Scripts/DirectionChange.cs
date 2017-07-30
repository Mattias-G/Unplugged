using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChange : MonoBehaviour {
	public enum Direction { Up, Left, Down, Right };
	public Direction newDirection = Direction.Right;
}
