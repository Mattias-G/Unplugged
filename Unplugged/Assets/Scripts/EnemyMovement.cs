using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelMovement))]
public class EnemyMovement : MonoBehaviour {

	public bool initialDirectionLeft;

	private WheelMovement wheelMovement;

	void Start ()
	{
		wheelMovement = GetComponent<WheelMovement>();
		wheelMovement.SetInput(1);
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
		wheelMovement.SetInput(Mathf.Sign(transform.localScale.x));
	}
}
