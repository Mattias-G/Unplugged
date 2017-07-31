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
        if (player) {
            var camera = Camera.main;
            var position = player.transform.position;
			position.y += 1;
			position.z = camera.transform.position.z;
			camera.transform.position = position;
        }
	}

    public void Detach()
    {
        player = null;
    }
}
