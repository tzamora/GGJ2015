using UnityEngine;
using System.Collections;

public class SimpleFlyingEnemyController : MonoBehaviour {

	public float speed;

	public int side = 0;

	// Use this for initialization
	void Start () {

		MoveRoutine ();
	
	}
	
	// Update is called once per frame
	void MoveRoutine () {
	
		this.ttAppendLoop ("MoveRoutine", delegate(){

			transform.Translate(new Vector3(side, 0f, 0f) * Time.deltaTime * speed);

		});

	}
}
