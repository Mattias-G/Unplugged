using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : StateMachineBehaviour {

    public GameObject faderPrefab;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		var movement = animator.gameObject.GetComponent<PlayerMovement>();
		movement.StopMoving();
		movement.enabled = false;
		var gun = animator.gameObject.GetComponent<ShootCable>();
		gun.enabled = false;
	}
    
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        var faderObject = (GameObject)Instantiate(faderPrefab, Vector3.zero, Quaternion.identity);
        var sceneFader = faderObject.GetComponent<SceneFadeOut>();
        sceneFader.targetScene = currentScene;
    }
}
