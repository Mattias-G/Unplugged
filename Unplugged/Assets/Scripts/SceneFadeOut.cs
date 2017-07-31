using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeOut : MonoBehaviour
{
	public Color fadeColor;
	public float timeBeforeFade = 0f;
	public float fadeTime = 2f;
	public float blackTime = 1f;
	public string targetScene;
	
	private Image fadingOverlay;
	private float fadeCounter;
	private float blackCounter;
    private float waitCounter;

	void Awake()
	{
		fadingOverlay = GetComponentInChildren<Image>();
		transform.position = Vector2.zero;
		DontDestroyOnLoad(gameObject);
		SceneManager.sceneLoaded += OnSceneWasLoaded;
	}
	
	protected virtual void Start ()
	{
		StartCoroutine(FadeAndLoadScene());
	}

	private IEnumerator FadeAndLoadScene()
	{
        yield return new WaitUntil(HasFinishedFading);
        yield return new WaitWhile(() => !CanSwitchScene());
        SceneManager.LoadScene(targetScene);
	}

	protected virtual bool CanSwitchScene()
	{
		return true; //!(waitForDialog && dialog.HasDialog());
	}

	public bool HasFinishedFading()
	{
		return fadeCounter >= fadeTime && blackCounter >= blackTime;
	}

	public virtual void Update()
	{
        if (waitCounter < timeBeforeFade) {
            waitCounter += Time.deltaTime;
        } else {
            fadeCounter += Time.deltaTime;
            fadeCounter = Mathf.Clamp(fadeCounter, 0, fadeTime);

            if (fadeCounter == fadeTime)
                blackCounter += Time.deltaTime;

            float alpha = fadeCounter / fadeTime;
            fadingOverlay.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
        }
	}

	protected virtual void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
	{
		var inFader = GameObject.FindObjectOfType<SceneFadeIn>();
		if (inFader)
			inFader.fadeColor = fadeColor;
		SceneManager.sceneLoaded -= OnSceneWasLoaded;
		Destroy(gameObject);
	}
}
