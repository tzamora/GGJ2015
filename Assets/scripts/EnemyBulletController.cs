using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {

	public GameObject bulletExplosionPrefab;

	void OnTriggerEnter2D(Collider2D other) {
		
		GameObject bulletExplosionGO = (GameObject)GameObject.Instantiate (bulletExplosionPrefab, transform.position, Quaternion.identity);
		
		float durationTime = bulletExplosionGO.GetComponent<ParticleSystem> ().duration;
		
		Destroy (bulletExplosionGO, durationTime);
		
	}

}
