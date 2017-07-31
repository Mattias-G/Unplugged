using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotUser : Activatable {
	protected Transform pivot;

	public override void Start() {
		base.Start();

		pivot = transform.GetChild(0);
		var list = new Transform[transform.childCount - 1];
		for (int i = 0; i < list.Length; i++) {
			list[i] = transform.GetChild(i + 1);
		}

		transform.DetachChildren();
		for (int i = 0; i < list.Length; i++) {
			list[i].SetParent(transform);
		}

		transform.SetParent(pivot, true);
	}
}
