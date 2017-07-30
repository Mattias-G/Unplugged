using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Activatable {

	public DirectionChange.Direction direction = DirectionChange.Direction.Left;
	[Range(0, 100)]
	public float speed = 1;

	private Vector3 DirectionToVector()
	{
		switch (direction) {
			case DirectionChange.Direction.Up:
				return new Vector3(0, 1, 0);
			case DirectionChange.Direction.Down:
				return new Vector3(0, -1, 0);
			case DirectionChange.Direction.Left:
				return new Vector3(-1, 0, 0);
			case DirectionChange.Direction.Right:
				return new Vector3(1, 0, 0);
		}

		return Vector2.zero;
	}

	void Update()
	{
		if (IsActive()) {
			var body = GetComponent<Rigidbody2D>();
			body.velocity = DirectionToVector() * speed;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var change = collider.gameObject.GetComponent<DirectionChange>();
		if (change) {
			direction = change.newDirection;
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		var body = collider.GetComponent<Rigidbody2D>();
		if (body) {
			//body.AddForce(DirectionToVector()*speed*4.5f);
			body.position += (Vector2)DirectionToVector() * speed * Time.deltaTime;
		}
	}
}
