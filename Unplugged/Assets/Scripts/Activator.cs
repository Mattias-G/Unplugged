using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activator : Activatable {
	public Activatable[] activatables = new Activatable[0];

	public override void Start() {
		base.Start();
		checkForMissingActivatable();
	}

	private void checkForMissingActivatable() {
		foreach (var activatable in activatables) {
			if (activatable == null) {
				Debug.Log("Activator \"" + name + "\" has null assigned as an activatable");
				return;
			}
		}
	}

	protected void ActivateObjects() {
		foreach (var activatable in activatables) {
			if (activatable) {
				activatable.Activate(gameObject);
			}
		}
	}

	protected void DeactivateObjects() {
		foreach (var activatable in activatables) {
			if (activatable) {
				activatable.Deactivate(gameObject);
			}
		}
	}

	public override void OnDrawGizmosSelected() {
		base.OnDrawGizmosSelected();
		Gizmos.color = Color.magenta;
		foreach (var activatable in activatables) {
			if (activatable) {
				Gizmos.DrawLine(transform.position, activatable.transform.position);
				DrawArrowBetween(transform.position, activatable.transform.position);
			}
		}
	}
}
