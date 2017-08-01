using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconCounter : MonoBehaviour {

	public static int count = 0;

	void Start () {
		
	}
	
	void Update ()
	{
		if (count == 4)
		{
			GameObject.Find("Last Goal").transform.position = new Vector3(59f, -16.6f, 0);
		}

		if (count == 5)
		{
			GetComponent<LevelEnder>().EndLevel();
		}
	}
}
