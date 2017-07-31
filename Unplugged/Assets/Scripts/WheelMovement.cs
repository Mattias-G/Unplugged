using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour {

	[Range(0f, 25f)]
	public float maxMovementSpeed = 3.5f;
	public float jumpBackUpForce = 50;
	public float jumpBackUpTorque = 0.5f;

	public delegate void OnMovement(float movementX);
	private OnMovement onMovement;

	private new Rigidbody2D rigidbody;
	private List<HingeJoint2D> wheels;

	private float input;
	private float stillTime = 0;
	private RaycastHit2D onGround;

	void Start ()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		wheels = new List<HingeJoint2D>();
		var wheelColliders = GetComponentsInChildren<CircleCollider2D>();
		foreach (var wheelCollider in wheelColliders) {
			var wheel = wheelCollider.GetComponent<HingeJoint2D>();
			if (wheel)
				wheels.Add(wheel);
		}
	}

	void FixedUpdate()
	{
		//Is on ground
		var pos = new Vector2(transform.position.x, transform.position.y);
		var dir = Vector2.down;
		var layer = LayerMask.GetMask("Ground");
		onGround = Physics2D.Raycast(pos, dir, 0.5f, layer);

		var lyingDown = Mathf.Abs(rigidbody.rotation) > 45;

		rigidbody.rotation = rigidbody.rotation % 360;
		if (rigidbody.rotation > 180)
			rigidbody.rotation -= 360;
		if (rigidbody.rotation <= -180)
			rigidbody.rotation += 360;


		if (!lyingDown && onGround) {
			SetSpeed(maxMovementSpeed * input);

			if (input != 0 && onMovement != null) {
				onMovement(input);
			}
		} else {
			SetSpeed(0);
		}

		if (onGround) {
			stillTime += Time.fixedDeltaTime;
			if (Mathf.Abs(rigidbody.angularVelocity) > 1) {
				stillTime = 0;
			}

			if (lyingDown && stillTime > 1) {
				stillTime = 0;
				rigidbody.AddForce(rigidbody.mass * jumpBackUpForce * Vector3.up);
				rigidbody.AddTorque(rigidbody.mass * jumpBackUpTorque * -Mathf.Min(Mathf.Abs(rigidbody.rotation), 120) * Mathf.Sign(rigidbody.rotation));
			}
		}
	}

	private void SetSpeed(float speed)
	{
		wheels.ForEach(wheel => {
			var angularSpeed = 360 * speed / (2 * Mathf.PI * wheel.GetComponent<CircleCollider2D>().radius);
			var motor = wheel.motor;
			motor.motorSpeed = angularSpeed;
			wheel.motor = motor;
		});
	}

	public void SetInput(float input)
	{
		this.input = input;
	}

	public void AddOnMovementCallback(OnMovement onMovement)
	{
		this.onMovement += onMovement;
	}

	public bool IsOnGround()
	{
		return onGround;
	}
}
