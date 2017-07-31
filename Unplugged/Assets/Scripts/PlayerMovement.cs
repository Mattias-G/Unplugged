using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelMovement))]
public class PlayerMovement : MonoBehaviour {

	private WheelMovement wheelMovement;
	private FeetDisplacement feet;
	private PlayerEnergy energy;

	void Start()
    {
		GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, -0.2f);
		wheelMovement = GetComponent<WheelMovement>();
		wheelMovement.AddOnMovementCallback(OnMovement);
		feet = transform.GetChild(1).GetComponent<FeetDisplacement>();
		energy = GetComponent<PlayerEnergy>();
	}
	
	void Update()
    {
		var x = Input.GetAxisRaw("Horizontal");
		wheelMovement.SetInput(x);

		if (x != 0) {
			var scale = transform.localScale;
			scale.x = Mathf.Sign(x);
			transform.localScale = scale;
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
