using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

	private Socket connectedSocket;
	
	void Start () {
	}
	
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var socket = collider.gameObject.GetComponent<Socket>();
		if (socket)
		{
			if (!connectedSocket)
			{
				var angleDiff = Quaternion.Angle(socket.gameObject.transform.rotation, gameObject.transform.rotation);

				if (Mathf.Abs(angleDiff) < 70)  // How big an interval should we allow?
				{
					var body = GetComponent<Rigidbody2D>();
					connectedSocket = socket;
					socket.connect(this);
					body.velocity = Vector2.zero;
					body.bodyType = RigidbodyType2D.Static;
				}
			}
		}
	}
}
