﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCable : MonoBehaviour {

	public Rigidbody2D cableSegment;
	public Rigidbody2D cablePlug;
	public int maxLength = 10;

	private new Rigidbody2D rigidbody;
	private Plug plug;
	private Stack<Rigidbody2D> segments;
	private Rigidbody2D segment;
	private SliderJoint2D slider;
	private Quaternion direction;
	private float shootingTimer;

	void Start () {
		rigidbody = GetComponentInChildren<HingeJoint2D>().GetComponent<Rigidbody2D>(); ;
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
		} else {
			if (Input.GetMouseButtonDown(1)) {
				if (plug.IsConnected()) {
					plug.Disconnect();
				}
			}
		}
	}

	void FixedUpdate() {
		shootingTimer -= Time.fixedDeltaTime;

		if (segment) {
			if (shootingTimer <= 0)
				slider.useLimits = UseSliderLimits;

			var vertical = Input.GetAxisRaw("Vertical");

			if (slider.jointTranslation > .5 && LengthRemaining > 0 && (vertical > 0 || IsShooting)) {
				//CreateSegment(segment.transform.position + segment.transform.right * .0f);
				CreateSegment(transform.position);
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
				SetMotorSpeed(Mathf.Sign(vertical) * 2);
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
		var plugBody = Instantiate<Rigidbody2D>(cablePlug, segment.transform.position + segment.transform.right, direction);
		plugBody.GetComponent<AnchoredJoint2D>().connectedBody = segment;
		plugBody.AddRelativeForce(new Vector2(20, 0), ForceMode2D.Impulse);
		GetComponent<PlayerEnergy>().SetPlug(plugBody.GetComponent<Plug>());
		plug = plugBody.GetComponent<Plug>();
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
