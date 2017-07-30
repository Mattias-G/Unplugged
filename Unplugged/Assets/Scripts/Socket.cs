using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour {

	private Plug connectedPlug;
	private float cooldown;

	private void Update() {
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
	}

	public void Connect(Plug plug)
	{
		connectedPlug = plug;
	}

	public void Disconnect()
	{
		connectedPlug = null;
		cooldown = .5f;
	}

	public bool CanConnect() {
		return cooldown <= 0;
	}
}
