using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Activator {

	private Plug connectedPlug;
	private float cooldown;
	private ParticleSystem sparkEffect;

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
	}

	public void Disconnect()
	{
		connectedPlug = null;
		cooldown = .5f;
		DeactivateObjects();
	}

	public bool CanConnect()
	{
		return cooldown <= 0;
	}
}
