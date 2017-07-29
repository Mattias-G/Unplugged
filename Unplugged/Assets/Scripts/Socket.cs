using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour {

	private Plug connectedPlug;

	public void connect(Plug plug)
	{
		connectedPlug = plug;
	}

	public void disconnect()
	{
		connectedPlug = null;
	}
}
