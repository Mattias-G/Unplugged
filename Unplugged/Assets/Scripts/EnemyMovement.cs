using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public bool initialDirectionLeft;

	private List<HingeJoint2D> wheels;

	void Start ()
	{
		wheels = new List<HingeJoint2D>(GetComponentsInChildren<HingeJoint2D>());
		if (initialDirectionLeft)
			FlipDirection();
	}

	void FixedUpdate()
	{
		int layers = LayerMask.GetMask("Ground");
		var right = transform.right * transform.localScale.x;
		var wallHit = Physics2D.Raycast(transform.position, right, .8f, layers);
		var groundHit = Physics2D.Raycast(transform.position, right * 2 - transform.up, 2f, layers);

		if (wallHit || !groundHit) {
			FlipDirection();
		}
	}

	private void FlipDirection()
	{
		transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
		wheels.ForEach(FlipSpeed);
	}

	private void FlipSpeed(HingeJoint2D wheel)
	{
		var motor = wheel.motor;
		motor.motorSpeed *= -1;
		wheel.motor = motor;
	}
}
