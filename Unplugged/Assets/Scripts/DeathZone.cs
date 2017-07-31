using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    public enum DeathCause {
        FallingOutOfWorld, Explosion
    }

    public DeathCause causeOfDeath = DeathCause.FallingOutOfWorld;
    
	void OnTriggerEnter2D(Collider2D collider)
    {
        var death = collider.GetComponent<PlayerDeath>();
        if (death) {
            switch (causeOfDeath) {
                case DeathCause.FallingOutOfWorld:
                    death.FellOutOfWorld();
                    break;
                case DeathCause.Explosion:
                    death.Explode();
                    break;
            }
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
