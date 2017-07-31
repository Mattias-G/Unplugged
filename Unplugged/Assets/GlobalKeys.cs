using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalKeys : MonoBehaviour {

	public GameObject faderPrefab;
	
	void Update ()
	{
		if (Input.GetButton("Restart")) {
			string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
			var sceneFader = faderObject.GetComponent<SceneFadeOut>();
			sceneFader.blackTime = 0;
			sceneFader.targetScene = currentScene;
		} else if (Input.GetButton("Quit")) {
			Application.Quit();
		}
	}
}
