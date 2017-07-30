using UnityEngine;
using System.Collections;

public class Door : Activatable
{
	public GameObject destination;
	public float timeToOpen = 1;

	private bool activated;
	private float timer;
	private Vector3 startPosition;
	private Vector3 endPosition;

	public override void Start ()
	{
		base.Start();
		startPosition = transform.position;
		endPosition = new Vector3(destination.transform.position.x, destination.transform.position.y, transform.position.z);
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
		transform.position = Vector3.Lerp(startPosition, endPosition, timer / timeToOpen);
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
