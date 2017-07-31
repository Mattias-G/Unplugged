using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {

	private Transform bg1l;
	private Transform bg1r;
	private Transform bg2l;
	private Transform bg2r;
	private Transform bg3l;
	private Transform bg3r;

	void Start () {
		bg1l = transform.GetChild(0);
		bg2l = transform.GetChild(1);
		bg3l = transform.GetChild(2);
		bg1r = transform.GetChild(3);
		bg2r = transform.GetChild(4);
		bg3r = transform.GetChild(5);
		transform.DetachChildren();
	}
	
	void FixedUpdate () {
		bg1l.position = new Vector3(transform.position.x - ((transform.position.x / 2) % 24), transform.position.y - Mathf.Max(-4, transform.position.y / 5), 0);
		bg1r.position = new Vector3(transform.position.x - ((transform.position.x / 2) % 24) + 24, transform.position.y - Mathf.Max(-4, transform.position.y / 5), 0);
		bg2l.position = new Vector3(transform.position.x - ((transform.position.x / 3) % 20), transform.position.y - Mathf.Max(-2, transform.position.y / 10), 0);
		bg2r.position = new Vector3(transform.position.x - ((transform.position.x / 3) % 20) + 20, transform.position.y - Mathf.Max(-2, transform.position.y / 10), 0);
		bg3l.position = new Vector3(transform.position.x - ((transform.position.x / 4) % 16), transform.position.y - Mathf.Max(-1, transform.position.y / 20), 0);
		bg3r.position = new Vector3(transform.position.x - ((transform.position.x / 4) % 16) + 16, transform.position.y - Mathf.Max(-1, transform.position.y / 20), 0);
	}
}
