using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Activator {

	private Plug connectedPlug;
	private float cooldown;

	private void Update()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

	public void Connect(Plug plug)
	{
		connectedPlug = plug;
		ActivateObjects();
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
