using UnityEngine;
using System.Collections;

public class GoalArea : MonoBehaviour
{
	public string NextLevel;
	public GameObject faderPrefab;
	public float fadeDuration = 2f;


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
			var sceneFader = faderObject.GetComponent<SceneFadeOut>();
			sceneFader.targetScene = NextLevel;
			sceneFader.fadeTime = fadeDuration;
		}
	}
}
