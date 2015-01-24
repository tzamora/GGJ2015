using UnityEngine;
using System.Collections;

public class CarriageLeverController : MonoBehaviour {

	public TriggerController LeftTrigger;

	public TriggerController RightTrigger;

	public Transform leverPivot;

	// Use this for initialization
	void Start () {
	
		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler){

			if(LeftTrigger.onEnter){

				if(Input.GetKey(KeyCode.A)){

					leverPivot.Rotate(new Vector3(0f, 0f, 15));
				}

			}

			if(RightTrigger.onEnter){
				
				if(Input.GetKey(KeyCode.A)){
					
					leverPivot.Rotate(new Vector3(0f, 0f, -15));
				}
				
			}


		});

	}
}
