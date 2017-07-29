using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Range(0f, 100f)]
	public float acceleration = 25f;
	[Range(0f, 25f)]
	public float maxMovementSpeed = 3.5f;
	public float walkingFriction = 0.93f;

	private Rigidbody2D playerBody;

	void Start () {
		playerBody = transform.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

	void FixedUpdate()
	{
		float playerMovementX = 0;
		float playerMovementY = 0;
		
		playerMovementX = Input.GetAxisRaw("Horizontal");
		//playerMovementY = Input.GetAxisRaw("Vertical");

		var playerAcceleration = new Vector2(playerMovementX, playerMovementY);
		if (playerAcceleration.sqrMagnitude > 1)
			playerAcceleration.Normalize();

		var dx = Mathf.Abs(playerAcceleration.x + playerBody.velocity.x) - maxMovementSpeed;
		if (dx < 0)
		{
			playerAcceleration *= acceleration * playerBody.mass;
			playerBody.AddForce(playerAcceleration);

			transform.GetChild(1).GetComponent<FeetDisplacement>().Move(playerMovementX / 2);
		}

		//Is on ground
		var pos = new Vector2(transform.position.x, transform.position.y);
		var dir = new Vector2(0, -1);
		var layer = 1 << LayerMask.NameToLayer("Ground");
		var hit = Physics2D.Raycast(pos, dir, 1f, layer);
		if (hit.collider != null)
		{
			playerBody.velocity *= walkingFriction;
		}
	}
}
