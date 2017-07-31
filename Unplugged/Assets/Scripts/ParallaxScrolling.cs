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
	private Transform textBackground;

	void Start ()
	{
		bg1l = transform.GetChild(0);
		bg2l = transform.GetChild(1);
		bg3l = transform.GetChild(2);
		bg1r = transform.GetChild(3);
		bg2r = transform.GetChild(4);
		bg3r = transform.GetChild(5);
		textBackground = transform.GetChild(6);
		transform.DetachChildren();
	}
	
	void FixedUpdate ()
	{
		var dx1 = ((transform.position.x / 2) % 24);
		if (dx1 < 0)
			dx1 += 24;
		var dx2 = ((transform.position.x / 3) % 20);
		if (dx2 < 0)
			dx2 += 20;
		var dx3 = ((transform.position.x / 4) % 16);
		if (dx3 < 0)
			dx3 += 16;

		bg1l.position = new Vector3(transform.position.x - dx1, transform.position.y - Mathf.Max(-3, transform.position.y / 5), 0);
		bg1r.position = new Vector3(transform.position.x - dx1 + 24, transform.position.y - Mathf.Max(-3, transform.position.y / 5), 0);
		bg2l.position = new Vector3(transform.position.x - dx2, transform.position.y - Mathf.Max(-2, transform.position.y / 10), 0);
		bg2r.position = new Vector3(transform.position.x - dx2 + 20, transform.position.y - Mathf.Max(-2, transform.position.y / 10), 0);
		bg3l.position = new Vector3(transform.position.x - dx3, transform.position.y - Mathf.Max(-1, transform.position.y / 20), 0);
		bg3r.position = new Vector3(transform.position.x - dx3 + 16, transform.position.y - Mathf.Max(-1, transform.position.y / 20), 0);
		textBackground.position = new Vector3(transform.position.x / 2, transform.position.y - Mathf.Max(-3, transform.position.y / 5), 0);
	}
}
