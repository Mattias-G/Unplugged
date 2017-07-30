using UnityEngine;
using System.Collections;

public class GizmosExtensions {
	public static void DrawArrow(Vector3 position, float directionInRadians, float scale = 0.2f) {
		Vector3 p1 = position - Vector3.right * Mathf.Cos(directionInRadians) * scale - Vector3.up * Mathf.Sin(directionInRadians) * scale;
		Vector3 p2 = position + Vector3.right * Mathf.Cos(directionInRadians) * scale + Vector3.up * Mathf.Sin(directionInRadians) * scale;
		Vector3 p3 = position + Vector3.right * Mathf.Cos(directionInRadians + Mathf.PI / 2) * scale / 2 + Vector3.up * Mathf.Sin(directionInRadians + Mathf.PI / 2) / 2 * scale;
		Vector3 p4 = position + Vector3.right * Mathf.Cos(directionInRadians - Mathf.PI / 2) * scale / 2 + Vector3.up * Mathf.Sin(directionInRadians - Mathf.PI / 2) / 2 * scale;

		Gizmos.DrawLine(p1, p2);
		Gizmos.DrawLine(p2, p3);
		Gizmos.DrawLine(p2, p4);
	}
}