using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{	
	public Color fadeColor;
	public float fadeTime = 2f;
	
	private Image fadingOverlay;
	private float fadeCounter;
	
	void Start ()
	{
		fadingOverlay = GetComponentInChildren<Image>();
		fadeCounter = fadeTime;
		transform.position = Vector2.zero;
	}

	void Update()
	{
		fadeCounter -= Time.deltaTime;
		if (fadeCounter <= 0)
		{
			fadeCounter = 0;
			Destroy(gameObject);
		}

		float alpha = fadeCounter / fadeTime;
		fadingOverlay.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
	}
}
