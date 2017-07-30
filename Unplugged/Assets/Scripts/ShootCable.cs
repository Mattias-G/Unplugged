﻿using System.Collections;
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
	private float shootingTimer;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		segments = new Stack<Rigidbody2D>();
	}
	
	void Update () {
		if (!segment) {
			if (Input.GetMouseButtonUp(0)) {
				shootingTimer = 0.6f;
				var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 delta = mousePosition - transform.position;
				var angle = Mathf.Atan2(delta.y, delta.x) * 180 / Mathf.PI;
				direction = Quaternion.Euler(0, 0, angle);
				CreateSegment(rigidbody.position + delta.normalized * 0.3f);
				CreatePlug();
			}

		}
	}

	void FixedUpdate() {
		shootingTimer -= Time.fixedDeltaTime;
		if (segment) {
			var vertical = Input.GetAxisRaw("Vertical");

			if (slider.jointTranslation > .5 && LengthRemaining > 0 && (vertical > 0 || IsShooting)) {
				CreateSegment(segment.transform.position + segment.transform.right * .1f);
			} else if (slider.jointTranslation < 0 && vertical < 0) {
				Destroy(segment.gameObject);
				if (segments.Count > 0) {
					segment = segments.Pop();
					segment.GetComponent<HingeJoint2D>().enabled = false;
					slider = segment.GetComponent<SliderJoint2D>();
					slider.enabled = true;
					slider.useLimits = UseSliderLimits;
				} else {
					Destroy(plug.gameObject);
					GetComponent<PlayerEnergy>().SetPlug(null);
					segment = null;
					plug = null;
				}
			}

			if (vertical != 0) {
				SetMotorSpeed(Mathf.Sign(vertical));
				slider.useMotor = true;
			} else {
				SetMotorSpeed(0);
				slider.useMotor = !IsShooting;
			}
		}
	}

	private void SetMotorSpeed(float speed) {
		var motor = slider.motor;
		motor.motorSpeed = speed;
		slider.motor = motor;
	}

	private void CreatePlug() {
		plug = Instantiate<Rigidbody2D>(cablePlug, segment.transform.position + segment.transform.right, direction);
		plug.GetComponent<AnchoredJoint2D>().connectedBody = segment;
		plug.AddRelativeForce(new Vector2(10, 0), ForceMode2D.Impulse);
		GetComponent<PlayerEnergy>().SetPlug(plug.GetComponent<Plug>());
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
		slider.useLimits = UseSliderLimits;
	}

	private int LengthRemaining {
		get {
			return maxLength - segments.Count - (segment ? 1 : 0);
		}
	}

	private bool UseSliderLimits {
		get {
			return LengthRemaining == 0 || !IsShooting;
		}
	}

	private bool IsShooting {
		get {
			return shootingTimer > 0;
		}
	}
}
