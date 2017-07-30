using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Range(0f, 100f)]
	public float acceleration = 25f;
	[Range(0f, 25f)]
	public float maxMovementSpeed = 3.5f;
	public float walkingFriction = 0.93f;
	public float jumpBackUpForce = 50;
	public float jumpBackUpTorque= 0.5f;


	private Rigidbody2D playerBody;

	private float stillTime = 0;

	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

	void FixedUpdate()
	{
		//Is on ground
		var pos = new Vector2(transform.position.x, transform.position.y);
		var dir = Vector2.down;
		var layer = 1 << LayerMask.NameToLayer("Ground");
		var hit = Physics2D.Raycast(pos, dir, 0.5f, layer);

		var lyingDown = Mathf.Abs(playerBody.rotation) > 45;

		float playerMovementX = 0;
		float playerMovementY = 0;

		playerBody.rotation = playerBody.rotation % 360;
		if (playerBody.rotation > 180)
			playerBody.rotation -= 360;
		if (playerBody.rotation <= -180)
			playerBody.rotation += 360;


		if (!lyingDown || hit.collider == null)
		{
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

				if (playerMovementX != 0)
				{
					transform.GetChild(1).GetComponent<FeetDisplacement>().Move(playerMovementX / 2);
					GetComponent<PlayerEnergy>().ChangeEnergy(-Time.deltaTime);
				}
			}
		}
		
		if (hit.collider != null)
		{
			playerBody.velocity *= walkingFriction;

			stillTime += Time.fixedDeltaTime;
			if (Mathf.Abs(playerBody.angularVelocity) > 1)
			{
				stillTime = 0;
			}

			if (lyingDown && stillTime > 1)
			{
				stillTime = 0;
				playerBody.AddForce(playerBody.mass * jumpBackUpForce * Vector3.up);
				playerBody.AddTorque(playerBody.mass * jumpBackUpTorque * -Mathf.Min(Mathf.Abs(playerBody.rotation), 120) * Mathf.Sign(playerBody.rotation));
			}

		}

	}
}
