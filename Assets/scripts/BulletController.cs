using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public GameObject bulletExplosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D(Collider2D other) {

		PlayerController player1 = other.GetComponent<PlayerController> ();

		//PlayerController2 player2 = other.GetComponent<PlayerController2> ();

		if (!(player1 /*|| player2*/)) {
		
			GameObject bulletExplosionGO = (GameObject)GameObject.Instantiate (bulletExplosionPrefab, transform.position, Quaternion.identity);
			
			float durationTime = bulletExplosionGO.GetComponent<ParticleSystem> ().duration;
			
			Destroy (bulletExplosionGO, durationTime);
		
		}

	}
}
