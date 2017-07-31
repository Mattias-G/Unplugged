using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	private GameObject player;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void LateUpdate ()
	{
		var camera = Camera.main;
		var position = player.transform.position;
		position.z = camera.transform.position.z;
		camera.transform.position = position;
	}
}
