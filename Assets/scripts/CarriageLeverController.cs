using UnityEngine;
using System.Collections;

public class CarriageLeverController : MonoBehaviour {

	public TriggerController LeftTrigger;

	public TriggerController RightTrigger;

	public Transform leverPivot;

	public bool leverIsLeft = false;

	// Use this for initialization
	void Start () {
	
		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler){

//			this.ttAppend("pumpLeft", delegate(){

				if(LeftTrigger.onEnter){
					
					if(Input.GetKeyDown(KeyCode.A)){
						
						if(leverIsLeft){
							
							leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, -20f));
							
							leverIsLeft = false;
							
						}else{
							
							leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
							
							leverIsLeft = true;
							
						}
						
					}
					
				}

//			}).ttLock();

//			if(LeftTrigger.onEnter){
//
//				if(Input.GetKeyDown(KeyCode.A)){
//
//					if(leverIsLeft){
//
//						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, -20f));
//						
//						leverIsLeft = false;
//					
//					}else{
//					
//						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
//						
//						leverIsLeft = true;
//					
//					}
//
//				}

//			}

			if(RightTrigger.onEnter){
				
				if(Input.GetKeyDown(KeyCode.A)){
					
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
