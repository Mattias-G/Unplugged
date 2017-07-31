using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelMovement))]
public class PlayerMovement : MonoBehaviour {

	public float swingForce;

	private new Rigidbody2D rigidbody;
	private WheelMovement wheelMovement;
	private FeetDisplacement feet;
	private PlayerEnergy energy;

	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.centerOfMass = new Vector2(0, -0.2f);
		wheelMovement = GetComponent<WheelMovement>();
		wheelMovement.AddOnMovementCallback(OnMovement);
		feet = transform.GetChild(1).GetComponent<FeetDisplacement>();
		energy = GetComponent<PlayerEnergy>();
	}
	
	void Update()
    {
		var x = Input.GetAxisRaw("Horizontal");
		wheelMovement.SetInput(x);

		if (x != 0 && wheelMovement.IsOnGround() && wheelMovement.IsStandingUp()) {
			var scale = transform.localScale;
			scale.x = Mathf.Sign(x);
			transform.localScale = scale;
		}
	}

	void FixedUpdate()
	{
		if (!wheelMovement.IsOnGround()) {
			var x = Input.GetAxisRaw("Horizontal");
			rigidbody.AddForce(Vector2.right * x * swingForce, ForceMode2D.Force);
		}
	}

	private void OnMovement(float movementX)
	{
		feet.Move(movementX / 2);
		energy.ChangeEnergy(-Time.fixedDeltaTime * Mathf.Abs(movementX));
	}

    public void StopMoving()
    {
        wheelMovement.SetInput(0);
    }
}
