using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour {

	public float maxEnergy = 10;
	public float energy;
	public Plug plug;

	private Transform energyMeter;
	private SpriteRenderer energyMeterSprite;

	void Start() {
		energy = maxEnergy;
		energyMeter = transform.GetChild(3);
		energyMeterSprite = energyMeter.GetComponent<SpriteRenderer>();
	}
	
	void Update() {
		var dEnergy = energy / maxEnergy;

		var r = Mathf.Min(1, 3 - dEnergy * 3);
		var g = Mathf.Max(0, dEnergy * 3 - 1);
		energyMeterSprite.color = new Vector4(r, g, 0, 1);

		if (plug != null && plug.IsConnected())
		{
			ChangeEnergy(Time.deltaTime * 4);
			energyMeterSprite.color = new Vector4(0, 1, 1, 1);
		}

		energyMeter.localScale = new Vector3(1, dEnergy, 1);
	}

	public void ChangeEnergy(float dEnergy)
	{
		energy += dEnergy;
		if (energy > maxEnergy)
			energy = maxEnergy;
		if (energy < 0)
			energy = 0;
	}

	public void SetPlug(Plug plug)
	{
		this.plug = plug;
	}
}
