using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public GameObject bulletPrefab;

	public float force = 100f;

	// Use this for initialization
	void Start () {
	


	}

	public void Shoot(){

		GameObject bulletGO = (GameObject)GameObject.Instantiate (bulletPrefab.gameObject, transform.position, Quaternion.identity);

		Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D> ();

		bulletRigidbody.velocity = new Vector3 (1f, 0f, 0f) * force;

	}
}
