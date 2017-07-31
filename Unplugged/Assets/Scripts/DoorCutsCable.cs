using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCutsCable : MonoBehaviour {

	void Start()
	{

	}

	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<CableData>()) {
			Destroy(other.gameObject);
		}
	}
}
