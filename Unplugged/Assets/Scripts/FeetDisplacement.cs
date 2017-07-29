using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetDisplacement : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y - transform.parent.GetComponent<Rigidbody2D>().velocity.y / 100, transform.position.z);
	}
}
