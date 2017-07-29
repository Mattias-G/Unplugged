using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCable : MonoBehaviour {

	public Rigidbody2D cableSegment;
	public Rigidbody2D cablePlug;
	public int maxLength = 10;

	private new Rigidbody2D rigidbody;
	private Rigidbody2D plug;
	private Stack<Rigidbody2D> segments;
	private Rigidbody2D segment;
	private SliderJoint2D slider;
	private Quaternion direction;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		segments = new Stack<Rigidbody2D>();
	}
	
	void Update () {
		if (segment) {
			if (slider.jointTranslation > .6 && LengthRemaining > 0) { 
				CreateSegment(segment.transform.position + segment.transform.right * .25f);
			} else if (slider.jointTranslation < 0.05) {
				Destroy(segment.gameObject);
				if (segments.Count > 0) {
					segment = segments.Pop();
					segment.GetComponent<HingeJoint2D>().enabled = false;
					slider = segment.GetComponent<SliderJoint2D>();
					slider.enabled = true;
				} else {
					Destroy(plug.gameObject);
					segment = null;
					plug = null;
				}
			}
		} else if (Input.GetMouseButtonUp(0)) {
			var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var delta = mousePosition - transform.position;
			var angle = Mathf.Atan2(delta.y, delta.x) * 180 / Mathf.PI;
			direction = Quaternion.Euler(0, 0, angle);
			CreateSegment(rigidbody.position);
			CreatePlug(angle + 0);
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

	private void CreatePlug(float angle) {
		plug = Instantiate<Rigidbody2D>(cablePlug, segment.transform.position + segment.transform.right, Quaternion.Euler(0, 0, angle));
		plug.GetComponent<AnchoredJoint2D>().connectedBody = segment;
		plug.AddRelativeForce(new Vector2(10, 0), ForceMode2D.Impulse);
	}

	private void CreateSegment(Vector3 position) {
		Rigidbody2D newSegment = Instantiate<Rigidbody2D>(cableSegment, position, direction);

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
	}

	private int LengthRemaining {
		get {
			return maxLength - segments.Count - (segment ? 1 : 0);
		}
	}
}
