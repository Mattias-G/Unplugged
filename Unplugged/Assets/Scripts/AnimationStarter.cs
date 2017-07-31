using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : Activatable {

	public string animationName = "";

	protected override void OnActivate() {
		GetComponent<Animator>().enabled = true;
	}
}
