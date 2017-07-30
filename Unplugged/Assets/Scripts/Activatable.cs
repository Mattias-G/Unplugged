using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {

	public int activationsRequired = 1;
	public bool inverted = false;

	private HashSet<GameObject> poweringSources = new HashSet<GameObject>();

	public virtual void Start() {
		UpdateActive();
	}

	public bool IsActive() {
		return (poweringSources.Count >= activationsRequired) ^ inverted; // ^ is xor
	}

	public void Activate(GameObject source) {
		bool changed = poweringSources.Add(source);
		if (changed && poweringSources.Count == activationsRequired) {
			UpdateActive();
		}
	}

	public void Deactivate(GameObject source) {
		bool changed = poweringSources.Remove(source);
		if (changed && poweringSources.Count == activationsRequired - 1) {
			UpdateActive();
		}
	}

	private void UpdateActive() {
		if (IsActive()) {
			OnActivate();
		} else {
			OnDeactivate();
		}
	}

	protected virtual void OnActivate() { }
	protected virtual void OnDeactivate() { }

	public int GetActivationCount() {
		return poweringSources.Count;
	}

	public virtual void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1f, 0.5f, 0f);

		var activators = GameObject.FindObjectsOfType<Activator>();
		DrawArrowsFromReferences<Activator, Activatable>(activators, a => a.activatables, a => a);
	}

	protected void DrawArrowsFromReferences<T1, T2>(T1[] objects, Func<T1, T2[]> getActivatables, Func<T2, Activatable> getActivatable) where T1 : MonoBehaviour {
		foreach (var o in objects) {

			if (Array.Exists(getActivatables.Invoke(o), a => getActivatable.Invoke(a) == this)) {
				Gizmos.DrawLine(transform.position, o.transform.position);
				DrawArrowBetween(o.transform.position, transform.position);
			}
		}
	}

	protected void DrawArrowBetween(Vector3 A, Vector3 B) {
		var difference = B - A;
		var direction = Mathf.Atan2(difference.y, difference.x);

		var center = (A + B) / 2;
		GizmosExtensions.DrawArrow(center, direction);
	}
}
