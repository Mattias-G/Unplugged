using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelMovement))]
public class EnemyMovement : MonoBehaviour {

	public bool initialDirectionLeft;

	private WheelMovement wheelMovement;
	private Socket socket;

	private float flipCooldown;

	void Start ()
	{
		GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, -0.2f);
		wheelMovement = GetComponent<WheelMovement>();
		wheelMovement.AddOnMovementCallback(OnMovement);
		wheelMovement.SetInput(1);
		if (initialDirectionLeft) {
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
			wheelMovement.SetInput(-1);
		}
		socket = GetComponentInChildren<Socket>();
	}

	void FixedUpdate()
	{
		bool powered = true;

		if (socket && socket.IsConnected())
			powered = false;

		flipCooldown -= Time.fixedDeltaTime;

		if (powered) {
			int layers = LayerMask.GetMask("Ground");
			var right = Vector2.right * transform.localScale.x;
			var wallHit = Physics2D.Raycast(transform.position, right, .8f, layers);
			var groundHit = Physics2D.Raycast(transform.position, right * 2 - Vector2.up, 2f, layers);

			var input = Mathf.Sign(transform.localScale.x);
			if ((wallHit || !groundHit) && flipCooldown < 0) {
				flipCooldown = 1;
				input = -input;
			}
			
			wheelMovement.SetInput(input);
		} else {
			wheelMovement.SetInput(0);
		}
	}

	private void OnMovement(float movementX)
	{
		var scale = transform.localScale;
		scale.x = Mathf.Sign(movementX);
		transform.localScale = scale;
	}
}
