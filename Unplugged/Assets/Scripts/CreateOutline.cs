using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOutline : MonoBehaviour {

	void Start () {
		var outline = transform.GetChild(0);
		outline.localScale = new Vector3(1 + 0.04f / transform.localScale.x,
			1 + 0.04f / transform.localScale.y,
			1 + 0.04f / transform.localScale.z);
	}
}
