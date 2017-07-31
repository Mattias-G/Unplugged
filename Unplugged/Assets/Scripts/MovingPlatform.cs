using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Activatable {

	public DirectionChange.Direction direction = DirectionChange.Direction.Left;
	[Range(0, 100)]
	public float speed = 1;
	private Vector2 currentSpeed;
	private Rigidbody2D body;

	public override void Start()
	{
		body = GetComponent<Rigidbody2D>();
		base.Start();
	}

	private Vector3 DirectionToVector()
	{
		switch (direction) {
			case DirectionChange.Direction.Up:
				return Vector2.up;
			case DirectionChange.Direction.Down:
				return Vector2.down;
			case DirectionChange.Direction.Left:
				return Vector2.left;
			case DirectionChange.Direction.Right:
				return Vector2.right;
		}

		return Vector2.zero;
	}

	void Update()
	{
		if (IsActive()) {
			var change = Mathf.Max(Time.deltaTime * 5, speed * Time.deltaTime/2);
			currentSpeed += (Vector2)DirectionToVector() * change;
			if (currentSpeed.magnitude > speed) {
				currentSpeed = currentSpeed.normalized * speed;
			}

		}
		else
		{
			currentSpeed *= 0.95f;
		}

		body.velocity = currentSpeed;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var change = collider.gameObject.GetComponent<DirectionChange>();
		if (change) {
			direction = change.newDirection;
			
			if (change.instantTurn) {
				var body = GetComponent<Rigidbody2D>();
				currentSpeed = DirectionToVector() * speed;
			}
		}
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		var body = collider.GetComponent<Rigidbody2D>();
		if (body) {
			//body.AddForce(DirectionToVector() * speed*0.1f);
			//body.position += (Vector2)DirectionToVector() * speed * Time.deltaTime;
		}
	}
}
