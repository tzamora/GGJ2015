using UnityEngine;
using System.Collections;

public class TriggerController : MonoBehaviour {

	public bool onEnter = false;

	public Collider2D other = false;

	void OnTriggerEnter2D(Collider2D theOther) {
		onEnter = true;
		other = theOther;
	}

	void OnTriggerExit2D(Collider2D theOther) {
		onEnter = theOther;
	}

}
