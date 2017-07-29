using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Range(0f, 100f)]
	public float acceleration = 5f;
	[Range(0f, 25f)]
	public float maxMovementSpeed = 2;

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

		UpdateSpeed(playerMovementX, playerMovementY);
	}

	private void UpdateSpeed(float playerMovementX, float playerMovementY)
	{
		var playerAcceleration = new Vector2(playerMovementX, playerMovementY);
		if (playerAcceleration.sqrMagnitude > 1)
			playerAcceleration.Normalize();

		playerAcceleration *= acceleration * playerBody.mass;
		playerBody.AddForce(playerAcceleration);
	}

}
