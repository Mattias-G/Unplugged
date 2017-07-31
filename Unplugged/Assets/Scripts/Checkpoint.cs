using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	private const string CheckpointTag = "Respawn";
	private static Vector3 respawnPoint;
	
	void Start()
	{
		if (respawnPoint != new Vector3(0, 0, 0))
			transform.position = respawnPoint;
	}
	
	public void UpdateCheckpoint(Transform target)
	{
		respawnPoint = target.position;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == CheckpointTag) {
			respawnPoint = collider.transform.position;
		}
	}
}
