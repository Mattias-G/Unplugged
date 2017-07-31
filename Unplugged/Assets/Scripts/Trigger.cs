using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : Activator {

	[Header("Activation")]
	public bool canActivate = true;
	public bool canDeactivate = false;

	private HashSet<GameObject> objects = new HashSet<GameObject>();


	void Update()
	{
		var isActive = objects.Count > 0;
		objects.RemoveWhere(item => item == null);

		if (objects.Count > 0 && !isActive)
		{
			if (canActivate)
				ActivateObjects();
		}

		if (objects.Count < 1 && isActive)
		{
			if (canDeactivate)
				DeactivateObjects();
		}
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (IsActive() && canActivate)
		{
			if (AcceptsObject(other))
			{
				objects.Add(other.gameObject);
				if (objects.Count == 1)
					ActivateObjects();
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (IsActive() && canDeactivate)
		{
			if (AcceptsObject(other))
			{
				objects.Remove(other.gameObject);
				if (objects.Count == 0)
					DeactivateObjects();
			}
		}
	}

	private bool AcceptsObject(Collider2D other)
	{
		return other.gameObject.tag == "Player";
	}
}
