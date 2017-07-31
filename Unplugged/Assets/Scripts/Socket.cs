using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Activator {

	private Plug connectedPlug;
	private AnchoredJoint2D joint;
	private ParticleSystem sparkEffect;

	private float cooldown;

	public Vector3 direction {
		get {
			float scale = 1;
			if (transform.parent) {
				scale = transform.parent.localScale.x;
			}
			var direction = transform.right * scale;
			return direction.normalized;
		}
	}

	public override void Start()
	{
		base.Start();
		joint = GetComponent<AnchoredJoint2D>();
		sparkEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

	public void Connect(Plug plug)
	{
		connectedPlug = plug;
		ActivateObjects();
		sparkEffect.Play();
		if (joint) {
			joint.enabled = true;
			joint.connectedBody = plug.GetComponent<Rigidbody2D>();
		}
	}

	public void Disconnect()
	{
		connectedPlug = null;
		cooldown = .5f;
		DeactivateObjects();
		if (joint)
			joint.enabled = false;
	}

	public bool IsConnected()
	{
		return connectedPlug != null;
	}

	public bool CanConnect()
	{
		return cooldown <= 0;
	}
}
