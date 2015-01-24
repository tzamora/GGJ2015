using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	public bool onEnter = false;
	public bool onExit = false;

	public Collider2D other = null;

	void OnTriggerEnter2D(Collider2D theOther) {
		onEnter = true;
		other = theOther;
	}

	void OnTriggerExit2D(Collider2D theOther) {
		onEnter = false;
		onExit = true;
		other = null;
	}

}
