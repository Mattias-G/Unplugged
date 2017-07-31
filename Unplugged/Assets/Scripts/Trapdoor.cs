using UnityEngine;
using System.Collections;

public class Trapdoor : PivotUser
{
	public float goalAngle;
	public float timeToOpen = 1;

	private bool activated;
	private float timer;
	private float startAngle;

	public override void Start()
	{
		startAngle = transform.rotation.eulerAngles.z;
		goalAngle = startAngle + goalAngle;
		base.Start();
	}

	void FixedUpdate()
	{
		if (activated && timer < timeToOpen)
		{
			timer += Time.fixedDeltaTime;
			if (timer >= timeToOpen) timer = timeToOpen;
		}
		if (!activated && timer > 0)
		{
			timer -= Time.fixedDeltaTime;
			if (timer < 0) timer = 0;
		}

		var angle = (goalAngle - startAngle) * timer / timeToOpen + startAngle;
		pivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}


	protected override void OnActivate()
	{
		activated = true;
	}

	protected override void OnDeactivate()
	{
		activated = false;
	}
}