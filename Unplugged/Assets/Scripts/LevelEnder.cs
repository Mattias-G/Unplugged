using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnder : MonoBehaviour {

	public string NextLevel;
	public GameObject faderPrefab;
	public float fadeDuration = 2f;


	public void EndLevel()
	{
		var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
		var sceneFader = faderObject.GetComponent<SceneFadeOut>();
		sceneFader.targetScene = NextLevel;
		sceneFader.fadeTime = fadeDuration;
	}
}
