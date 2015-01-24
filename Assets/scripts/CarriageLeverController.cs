using UnityEngine;
using System.Collections;

public class CarriageLeverController : MonoBehaviour {

	public TriggerController LeftTrigger;

	public TriggerController RightTrigger;

	// Use this for initialization
	void Start () {
	
		this.ttAppendLoop ("PumpLeverRoutine", delegate(ttHandler handler){



		});

	}
}
