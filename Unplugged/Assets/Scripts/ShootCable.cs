﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCable : MonoBehaviour {

	public Rigidbody2D cableSegment;
	public Rigidbody2D cablePlug;
	public int maxLength = 10;
	public float segmentLength = .3f;
	public float force = 30;

	private new Rigidbody2D rigidbody;
	private Plug plug;
	private Stack<Rigidbody2D> segments;
	private Rigidbody2D segment;
	private SliderJoint2D slider;
	private Quaternion direction;
	private float shootingTimer;

	void Start () {
		rigidbody = transform.GetChild(4).GetComponent<Rigidbody2D>(); ;
		segments = new Stack<Rigidbody2D>();
	}
	
	void Update () {
		if (!segment) {
			if (Input.GetMouseButtonUp(0)) {
				shootingTimer = 1.0f;
				var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector2 delta = mousePosition - transform.position;
				var angle = Mathf.Atan2(delta.y, delta.x) * 180 / Mathf.PI;
				direction = Quaternion.Euler(0, 0, angle);
				CreateSegment(rigidbody.position);
				CreatePlug();
				segment.GetComponent<CableData>().SetPlug(plug.GetComponent<Plug>());
			}
		} else {
			if (Input.GetMouseButtonDown(1)) {
				plug.Disconnect();
			}
		}
	}

	void FixedUpdate() {
		shootingTimer -= Time.fixedDeltaTime;

		if (segment) {
			if (shootingTimer <= 0 && slider)
				slider.useLimits = UseSliderLimits;

			var vertical = Input.GetAxisRaw("Vertical");

			if (slider.jointTranslation >= segmentLength && LengthRemaining > 0 && (vertical > 0 || IsShooting)) {
				CreateSegment(rigidbody.position);
				segment.GetComponent<CableData>().SetPlug(plug.GetComponent<Plug>());
			} else if (slider.jointTranslation < 0 && vertical < 0) {
				RemoveSegment();
			}

			if (vertical != 0) {
				SetMotorSpeed(Mathf.Sign(vertical) * 2);
				slider.useMotor = true;
			} else {
				SetMotorSpeed(0);
				slider.useMotor = !IsShooting;
			}
		} else if (segments.Count > 0) {
			segments.Clear();
			plug.Disconnect();
			GetComponent<PlayerEnergy>().SetPlug(null);
		}
	}

	private void RemoveSegment()
	{
		Destroy(segment.gameObject);
		segment = null;

		if (segments.Count == 0) {
			plug.Disconnect();
			Destroy(plug.gameObject);
			GetComponent<PlayerEnergy>().SetPlug(null);
			plug = null;
		}

		while (!segment && segments.Count > 0) {
			segment = segments.Pop();
			if (segment) {
				var hinge = segment.GetComponent<HingeJoint2D>();
				if (hinge)
					hinge.enabled = false;
				slider = segment.GetComponent<SliderJoint2D>();
				slider.enabled = true;
				slider.useLimits = UseSliderLimits;
			} else {
				segments.Clear();
				plug.Disconnect();
				GetComponent<PlayerEnergy>().SetPlug(null);
			}
		}
	}

	private void SetMotorSpeed(float speed) {
		var motor = slider.motor;
		motor.motorSpeed = speed;
		slider.motor = motor;
	}

	private void CreatePlug() {
		var plugBody = Instantiate<Rigidbody2D>(cablePlug, segment.transform.position + segment.transform.right.normalized * 0.1f, direction);
		plugBody.GetComponent<AnchoredJoint2D>().connectedBody = segment;
		plugBody.AddForce(plugBody.transform.right * force, ForceMode2D.Impulse);
		rigidbody.AddForce(plugBody.transform.right * -force / 2, ForceMode2D.Impulse);
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
