using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TriggerController : MonoBehaviour {

	public bool onEnter = false;
	public bool onExit = false;

	public Action<Collider> OnEnter;

	public List<Collider2D> others = null;

	void OnTriggerEnter2D(Collider2D theOther) {
		onEnter = true;
		others.Add(theOther);
	}

	void OnTriggerExit2D(Collider2D theOther) {
		others.Remove (theOther);

		if (others.Count <= 0) {
		
			onEnter = false;
			onExit = true;
		
		}
	}

}
