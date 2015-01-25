using UnityEngine;
using System.Collections;

public class CarriageLeverController : MonoBehaviour {

	public TriggerController LeftTrigger;

	public TriggerController RightTrigger;

	public Transform leverPivot;

	public bool leverIsLeft = false;

	public int leverPushCounter = 0;

	public int maxCounter = 15;

	public Transform[] wheels;

	public bool enemyCarriage = false;

	// Use this for initialization
	void Start () {

		if (enemyCarriage) {
		
			AppearanceRoutine ();

		}
	
		PushLeverRoutine ();

		EnemyMoveLeverRoutine ();

		SetGUICounterRoutine ();

		ReduceCounterRoutine ();

		SetBackgroundSpeedRoutine ();

		MoveWheelsRoutine ();

	}

	void AppearanceRoutine (){

		// 

		this.ttAppend("AppearanceRoutine", 4f).ttAppendLoop (2f, delegate(ttHandler handler) {

			transform.Translate(new Vector3 (-1f * 2f, 0f, 0f) * Time.deltaTime);

		});

	}

	void SetGUICounterRoutine (){

		this.ttAppendLoop ("SetGUICounterRoutine", delegate(ttHandler handler) {

			GameContext.Get.GUI.counterText.text = leverPushCounter+"";

		});

	}

	void ReduceCounterRoutine(){

		this.ttAppendLoop ("ReduceCounterRoutine", delegate(ttHandler handler) {
		
			handler.WaitFor(0.3f);

			leverPushCounter--;

			leverPushCounter = Mathf.Clamp(leverPushCounter, 0, 15);
		
		});

	}

	void PushLeverRoutine(){

		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler){

			if(LeftTrigger.onEnter){
				
				if(Input.GetKeyDown(KeyCode.A)){

				leverPushCounter++;

					leverPushCounter = Mathf.Clamp(leverPushCounter, 0, 15);
					
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

					leverPushCounter = Mathf.Clamp(leverPushCounter, 0, 15);
					
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

	void EnemyMoveLeverRoutine(){
		
		this.ttAppendLoop ("EnemyMoveLeverRoutine", delegate(ttHandler handler){
			
			if(RightTrigger.onEnter){
				
				EnemyController enemy = RightTrigger.other.GetComponent<EnemyController>();
				
				if(enemy != null){
					
					leverPushCounter++;
					
					leverPushCounter = Mathf.Clamp(leverPushCounter, 0, 15);
					
					if(leverIsLeft){
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, -20f));
						
						leverIsLeft = false;
						
					}else{
						
						leverPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, 20f));
						
						leverIsLeft = true;
						
					}
					
				}
				
			}

			handler.WaitFor(0.5f);
			
			
		});
		
	}

	void MoveWheelsRoutine(){

		this.ttAppendLoop ("MoveWheelsRoutine", delegate(ttHandler handler) {
				
			float t = Mathf.InverseLerp(0, 15, GameContext.Get.playerCarriage.leverPushCounter);
			
			for (int i = 0; i < wheels.Length; i++) {
				
				wheels[i].Rotate(new Vector3(0f,0f,-t*15));
				
			}	
		
		});

	}

	void SetBackgroundSpeedRoutine (){

		this.ttAppendLoop ("SetBackgroundSpeedRoutine", delegate(ttHandler handler) {
		
			float t = Mathf.InverseLerp(0, 15, leverPushCounter);

			for (int i = 0; i < wheels.Length; i++) {

				wheels[i].Rotate(new Vector3(0f,0f,-t*15));

			}

			GameContext.Get.background.ScrollingSpeed = -Mathf.Lerp(0f, 10f, t);
		
		});

	}
}
