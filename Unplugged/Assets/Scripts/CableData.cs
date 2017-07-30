using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableData : MonoBehaviour {

	private Plug plug;
	private bool electrified;

	void Update() {
		if (plug != null && plug.IsConnected() && !electrified) { 
			transform.GetChild(2).GetComponent<ParticleSystem>().Play();
			electrified = true;
		} else if ((plug == null || !plug.IsConnected()) && electrified) {
			transform.GetChild(2).GetComponent<ParticleSystem>().Stop();
			electrified = false;
		}
	}

	public void SetPlug(Plug plug) {
		this.plug = plug;
	}
}
