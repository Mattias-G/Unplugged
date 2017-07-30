using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

	private const float LerpTimeSeconds = 0.1f;

	private Socket connectedSocket;

	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private Vector3 targetPosition;
	private Quaternion targetRotation;
	private float lerp;

	void Start () {
	}
	
	void Update () {
		if (connectedSocket && lerp < 1)
		{
			lerp = Mathf.Min(1f, lerp + Time.deltaTime / LerpTimeSeconds);
			transform.position = Vector3.Lerp(initialPosition, targetPosition, lerp);
			transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, lerp);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var socket = collider.gameObject.GetComponent<Socket>();
		if (socket)
		{
			if (!connectedSocket)
			{
				var angleDiff = Quaternion.Angle(socket.transform.rotation, transform.rotation);

				if (Mathf.Abs(angleDiff) < 70)  // How big an interval should we allow?
				{
					connectedSocket = socket;
					socket.connect(this);

					var body = GetComponent<Rigidbody2D>();
					body.velocity = Vector2.zero;
					body.bodyType = RigidbodyType2D.Static;

					initialPosition = transform.position;
					initialRotation = transform.rotation;

					targetPosition = socket.transform.position - socket.transform.up*0.01f;
					targetRotation = socket.transform.rotation;
				}
			}
		}
	}

	public bool IsConnected()
	{
		return connectedSocket != null;
	}
}
