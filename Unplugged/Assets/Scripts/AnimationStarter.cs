using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarter : Activatable {
	
	protected override void OnActivate() {
		GetComponent<Animator>().enabled = true;
		BeaconCounter.count++;
	}
}
