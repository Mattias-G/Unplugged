using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public GameObject faderPrefab;
    
	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) {
            var camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<FollowPlayer>().Detach();
            
            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
            var sceneFader = faderObject.GetComponent<SceneFadeOut>();
            sceneFader.targetScene = currentScene;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, transform.lossyScale);
    }
}
