using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public GameObject bulletPrefab;

	public float force = 100f;

	// Use this for initialization
	void Start () {
	


	}

	public void Shoot(Vector3 direction){

		GameObject bulletGO = (GameObject)GameObject.Instantiate (bulletPrefab.gameObject, transform.position, Quaternion.identity);

		Rigidbody2D bulletRigidbody = bulletGO.GetComponent<Rigidbody2D> ();

		bulletRigidbody.velocity = direction * force ;

	}
}
