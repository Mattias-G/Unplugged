using UnityEngine;
using System.Collections;
using System;

public class AutomaticActivator : Activator
{
	public float onTime = 1;
	public float offTime = 1;
	public float initialTime = 0;
	public bool startsOn = false;

	protected float timer;
	protected bool isOn;


	void OnValidate()
	{
		if (onTime < 0)
			onTime = 0;
		if (offTime < 0)
			offTime = 0;
		if (initialTime < 0)
			initialTime = 0;
	}


	void Awake()
	{
		timer = initialTime;
		isOn = startsOn;
	}


	public override void Start ()
	{
		base.Start();
		TriggerActivatables();
	}


	protected void Reset()
	{
		ChangeState(startsOn);
		timer = initialTime;
	}


	protected virtual void Update()
	{
		if (IsActive())
		{
			timer += Time.deltaTime;
			UpdateState();
		}
	}

	private void UpdateState()
	{
		if (isOn)
		{
			if (timer > onTime)
			{
				ChangeState(false);
			}
		}
		else
		{
			if (timer > offTime)
			{
				ChangeState(true);
			}
		}
	}

	protected virtual void ChangeState(bool newState)
	{
		timer = 0;
		isOn = newState;
		TriggerActivatables();
	}

	private void TriggerActivatables()
	{
		if (isOn)
			ActivateObjects();
		else
			DeactivateObjects();
	}
}
