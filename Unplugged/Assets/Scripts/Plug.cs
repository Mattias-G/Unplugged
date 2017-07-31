using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour {

	private const float LerpTimeSeconds = 0.1f;

	private Socket connectedSocket;
	private new Rigidbody2D rigidbody;

	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private float lerp;

	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		if (connectedSocket)
		{
			lerp = Mathf.Clamp01(lerp + Time.deltaTime / LerpTimeSeconds);
			var targetPosition = connectedSocket.transform.position - connectedSocket.transform.up*0.01f;
			var targetRotation = Quaternion.FromToRotation(Vector3.right, connectedSocket.direction);
			transform.position = Vector3.Lerp(initialPosition, targetPosition, lerp);
			transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, lerp);
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		var socket = collider.gameObject.GetComponent<Socket>();
		if (socket && socket.CanConnect())
		{
			if (!connectedSocket)
			{
				//var angleDiff = Quaternion.Angle(socket.transform.rotation, transform.rotation);
				var dot = Vector2.Dot(socket.direction, transform.right.normalized);

				if (dot > -.2f)  // How big an interval should we allow?
				{
					connectedSocket = socket;
					socket.Connect(this);

					rigidbody.velocity = Vector2.zero;
					rigidbody.bodyType = RigidbodyType2D.Static;

					initialPosition = transform.position;
					initialRotation = transform.rotation;
				}
			}
		}
	}

	public bool IsConnected()
	{
		return connectedSocket != null;
	}

	public void Disconnect() {
		connectedSocket.Disconnect();
		connectedSocket = null;
		rigidbody.bodyType = RigidbodyType2D.Dynamic;
		lerp = 0;
	}
}
