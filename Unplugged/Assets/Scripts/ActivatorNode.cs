using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorNode : Activator {

	protected override void OnActivate()
	{
		ActivateObjects();
	}

	protected override void OnDeactivate()
	{
		DeactivateObjects();
	}
}
