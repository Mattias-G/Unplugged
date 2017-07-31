using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Range(0f, 25f)]
	public float maxMovementSpeed = 3.5f;
	public float jumpBackUpForce = 50;
	public float jumpBackUpTorque= 0.5f;

	private float angularSpeed;

	private Rigidbody2D playerBody;
	private FeetDisplacement feet;
	private PlayerEnergy energy;
	private List<HingeJoint2D> wheels;

	private float stillTime = 0;

	void Start () {
		playerBody = GetComponent<Rigidbody2D>();
		playerBody.centerOfMass = new Vector2(0, -0.2f);

		feet = transform.GetChild(1).GetComponent<FeetDisplacement>();
		energy = GetComponent<PlayerEnergy>();
		wheels = new List<HingeJoint2D>();
		wheels.Add(transform.GetChild(5).GetComponent<HingeJoint2D>());
		wheels.Add(transform.GetChild(6).GetComponent<HingeJoint2D>());
		angularSpeed = 360 * maxMovementSpeed / (2 * Mathf.PI * GetComponentInChildren<CircleCollider2D>().radius);
	}
	
	void Update () {
		
	}

	void FixedUpdate()
	{
		//Is on ground
		var pos = new Vector2(transform.position.x, transform.position.y);
		var dir = Vector2.down;
		var layer = LayerMask.GetMask("Ground");
		var isOnGround = Physics2D.Raycast(pos, dir, 0.5f, layer);

		var lyingDown = Mathf.Abs(playerBody.rotation) > 45;

		float playerMovementX = 0;

		playerBody.rotation = playerBody.rotation % 360;
		if (playerBody.rotation > 180)
			playerBody.rotation -= 360;
		if (playerBody.rotation <= -180)
			playerBody.rotation += 360;


		if (!lyingDown || !isOnGround)
		{
			playerMovementX = Input.GetAxisRaw("Horizontal");
			SetSpeed(angularSpeed * playerMovementX);

			if (playerMovementX != 0) {
				feet.Move(playerMovementX / 2);
				energy.ChangeEnergy(-Time.deltaTime);
			}
		}
		
		if (isOnGround)
		{
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

	private void SetSpeed(float speed)
	{
		wheels.ForEach(wheel => {
			var motor = wheel.motor;
			motor.motorSpeed = speed;
			wheel.motor = motor;
		});
	}
}
