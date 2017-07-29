using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour {

	public float maxEnergy = 10;
	public float energy;

	void Start() {
		energy = maxEnergy;
	}
	
	void Update() {
		var dEnergy = energy / maxEnergy;

		var r = Mathf.Min(1, 3 - dEnergy * 3);
		var g = Mathf.Max(0, dEnergy * 3 - 1);
		transform.GetChild(3).GetComponent<SpriteRenderer>().color = new Vector4(r, g, 0, 1);	
	}

	public void ChangeEnergy(float dEnergy)
	{
		energy += dEnergy;
		if (energy > maxEnergy)
			energy = maxEnergy;
		if (energy < 0)
			energy = 0;
	}
}
