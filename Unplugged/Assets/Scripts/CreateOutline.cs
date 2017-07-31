using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOutline : MonoBehaviour {

	private Transform outline;

	void Start ()
	{
		outline = transform.GetChild(0);
	}

	private void Update()
	{
		outline.localScale = new Vector3(1 + 0.04f / transform.localScale.x,
			1 + 0.04f / transform.localScale.y,
			1 + 0.04f / transform.localScale.z);
	}
}
