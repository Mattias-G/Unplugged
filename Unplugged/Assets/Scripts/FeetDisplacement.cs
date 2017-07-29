using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetDisplacement : MonoBehaviour {

	private float feetAngle = 0;

	void Start () {
		
	}
	
	void Update () {
		transform.position = new Vector3(transform.parent.position.x + Mathf.Sin(feetAngle) / 100, 
			transform.parent.position.y - transform.parent.GetComponent<Rigidbody2D>().velocity.y / 100, 
			transform.position.z);
	}

	public void Move(float speed)
	{
		feetAngle += speed;
		if (feetAngle > Mathf.PI * 2)
			feetAngle -= Mathf.PI * 2;
		if (feetAngle < 0)
			feetAngle += Mathf.PI * 2;

		transform.parent.GetChild(0).position = new Vector3(transform.parent.position.x,
			transform.parent.position.y - Mathf.Cos((int)feetAngle) / 100,
			transform.position.z);
		transform.parent.GetChild(2).position = new Vector3(transform.parent.position.x,
			transform.parent.position.y - Mathf.Cos((int)feetAngle) / 100,
			transform.position.z);
		transform.parent.GetChild(3).position = new Vector3(transform.parent.position.x,
			transform.parent.position.y - Mathf.Cos((int)feetAngle) / 100,
			transform.position.z);
	}
}
