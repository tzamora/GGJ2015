using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class SimpleFlyingEnemyController : MonoBehaviour {

	public float speed;

	public int side = 0;

	// Use this for initialization
	void Start () {

		MoveRoutine ();

		ShakeRoutine ();
	}

	void ShakeRoutine(){

		float speed = 0.5f;
		
		this.ttAppendLoop ("ShakeRoutine", delegate(ttHandler handler){

			transform.position = Vector3.Lerp(transform.position,
			                                  transform.position + new Vector3((Random.value-0.5f) * speed, 0, 
			                             (Random.value-0.5f)*speed), Time.time);

		});



	}

	// Update is called once per frame
	void MoveRoutine () {
	
//		this.ttAppendLoop ("MoveRoutine", delegate(ttHandler ttHandler){
//
//			transform.Translate(new Vector3(side, 0f, 0f) * Time.deltaTime * speed);
//
//		});

	}
}
