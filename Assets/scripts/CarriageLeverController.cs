using UnityEngine;
using System.Collections;

public class CarriageLeverController : MonoBehaviour {

	public TriggerController LeftTrigger;

	public TriggerController RightTrigger;

	public Transform leverPivot;

	public bool leverIsLeft = false;

	public int leverPushCounter = 0;

	// Use this for initialization
	void Start () {
	
		PushLeverRoutine ();

		SetGUICounterRoutine ();

		ReduceCounterRoutine ();

	}

	void SetGUICounterRoutine (){

		this.ttAppendLoop ("SetGUICounterRoutine", delegate(ttHandler handler) {

			GameContext.Get.GUI.counterText.text = leverPushCounter+"";

		});

	}

	void ReduceCounterRoutine(){

		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler) {
		
			handler.WaitFor(0.3f);

			leverPushCounter--;
		
		});

	}

	void PushLeverRoutine(){

		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler){

			if(LeftTrigger.onEnter){
				
				if(Input.GetKeyDown(KeyCode.A)){

				leverPushCounter++;
					
					if(leverIsLeft){
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, -20f));
						
						leverIsLeft = false;
						
					}else{
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
						
						leverIsLeft = true;
						
					}
					
				}
				
			}

			if(RightTrigger.onEnter){
				
				if(Input.GetKeyDown(KeyCode.A)){

					leverPushCounter++;
					
					if(leverIsLeft){
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, -20f));
						
						leverIsLeft = false;
						
					}else{
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
						
						leverIsLeft = true;
						
					}
					
				}
				
			}


		});

	}
}
