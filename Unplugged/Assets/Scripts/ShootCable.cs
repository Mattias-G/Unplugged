using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCable : MonoBehaviour {

	public Rigidbody2D cableSegment;

	private new Rigidbody2D rigidbody;
	private Rigidbody2D segment;
	private SliderJoint2D slider;
	private HingeJoint2D hinge;
	private Quaternion direction;
	private int lengthRemaining;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var delta = mousePosition - transform.position;
			var angle = Mathf.Atan2(delta.y, delta.x);
			direction = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
			lengthRemaining = 5;
			segment = null;
			ShootSegment(rigidbody.position);
			segment.AddRelativeForce(new Vector2(2, 0), ForceMode2D.Impulse);
		}

		
		if (segment && slider.jointTranslation > .9) {
			if (lengthRemaining > 0)
				ShootSegment(segment.transform.position - segment.transform.forward);
			else {
				slider.enabled = false;
				var hinge = segment.GetComponent<HingeJoint2D>();
				hinge.enabled = true;
				hinge.connectedBody = rigidbody;
				hinge.autoConfigureConnectedAnchor = true;
				//hinge.connectedAnchor = Vector2.c
				print(hinge.connectedAnchor);
			}
		}
	}

	private void ShootSegment(Vector3 position) {
		var newSegment = Instantiate<Rigidbody2D>(cableSegment, position, direction);

		if (segment) {
			segment.GetComponent<SliderJoint2D>().enabled = false;
			var hinge = segment.GetComponent<HingeJoint2D>();
			hinge.enabled = true;
			hinge.connectedBody = newSegment;
		}

		segment = newSegment;
		slider = segment.GetComponent<SliderJoint2D>();
		slider.enabled = true;
		slider.connectedBody = rigidbody;
		hinge = segment.GetComponent<HingeJoint2D>();
		//hinge.enabled = true;
		//hinge.connectedBody = rigidbody;

		lengthRemaining--;
	}
}
