using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    public GameObject playerPartsPrefab;
    public GameObject faderPrefab;

    public void FellOutOfWorld()
    {
        DetachCamera();
        FadeAndRestart(0);
    }

    public void Explode()
    {
        var parts = Instantiate(playerPartsPrefab, transform.position, transform.rotation);
        parts.transform.localScale = transform.localScale;
        gameObject.SetActive(false);

        const float range = 2;

        var colliders = Physics2D.OverlapCircleAll(parts.transform.position, range);
        foreach (Collider2D collider in colliders) {
            var direction = (collider.transform.position - parts.transform.position).normalized;
            float distance = Vector2.Distance(collider.transform.position, parts.transform.position);
            var body = collider.GetComponent<Rigidbody2D>();
            if (body && distance < range) {
                var force = Mathf.Pow(1 - distance/range, 2) * (range - distance) * body.mass * direction * 300;
                body.AddForce(force );
            }
        }

        FadeAndRestart(1);
    }

    private void DetachCamera()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.GetComponent<FollowPlayer>().Detach();
    }

    private void FadeAndRestart(float secondsBeforeFade)
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
        var sceneFader = faderObject.GetComponent<SceneFadeOut>();
        sceneFader.timeBeforeFade = secondsBeforeFade;
        sceneFader.targetScene = currentScene;
    }
}
