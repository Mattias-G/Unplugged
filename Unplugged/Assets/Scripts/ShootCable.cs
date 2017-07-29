using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCable : MonoBehaviour {

	public Rigidbody2D cableSegment;
	public int maxLength = 5;

	private new Rigidbody2D rigidbody;
	private Stack<Rigidbody2D> segments;
	private Rigidbody2D segment;
	private SliderJoint2D slider;
	private Quaternion direction;
	//private bool justAdded, justRemoved;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		segments = new Stack<Rigidbody2D>();
	}
	
	void Update () {
		if (segment) {
			//if (slider.jointTranslation > .3)
			//	justAdded = false;
			//if (slider.jointTranslation < .2)
			//	justRemoved = false;

			if (slider.jointTranslation > .6 && LengthRemaining > 0) { 
				ShootSegment(segment.transform.position - segment.transform.forward);
			} else if (slider.jointTranslation < 0.05) {
				Destroy(segment.gameObject);
				if (segments.Count > 0) {
					//justRemoved = true;
					segment = segments.Pop();
					segment.GetComponent<HingeJoint2D>().enabled = false;
					slider = segment.GetComponent<SliderJoint2D>();
					slider.enabled = true;
				} else {
					segment = null;
				}
			}
		} else if (Input.GetMouseButtonUp(0)) {
			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var delta = mousePosition - transform.position;
			var angle = Mathf.Atan2(delta.y, delta.x);
			direction = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
			ShootSegment(rigidbody.position);
			segment.AddRelativeForce(new Vector2(2, 0), ForceMode2D.Impulse);
		}
	}

	void FixedUpdate() {
		if (segment) {
			var vertical = Input.GetAxisRaw("Vertical");
			if (vertical != 0) {
				slider.useMotor = true;
				var motor = slider.motor;
				motor.motorSpeed = Mathf.Sign(vertical);
				slider.motor = motor;
			} else {
				slider.useMotor = false;
			}
		}
	}

	private void ShootSegment(Vector3 position) {
		var newSegment = Instantiate<Rigidbody2D>(cableSegment, position, direction);

		if (segment) {
			segments.Push(segment);
			segment.GetComponent<SliderJoint2D>().enabled = false;
			var hinge = segment.GetComponent<HingeJoint2D>();
			hinge.enabled = true;
			hinge.connectedBody = newSegment;
		}

		segment = newSegment;
		slider = segment.GetComponent<SliderJoint2D>();
		slider.enabled = true;
		slider.connectedBody = rigidbody;
		slider.connectedAnchor = Vector2.zero;
		slider.useLimits = LengthRemaining == 0;

		//justAdded = true;
	}

	private int LengthRemaining {
		get {
			return maxLength - segments.Count - (segment ? 1 : 0);
		}
	}
}
