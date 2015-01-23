using UnityEngine;
using System.Collections;
using System;

public class TriggerListener : MonoBehaviour {

	public Action<GameObject> OnEnter;

	public Action<GameObject> OnStay;

	public Action<GameObject> OnExit;

	void OnTriggerEnter(Collider other)
	{
		if(OnEnter != null)
		{
			OnEnter(other.gameObject);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(OnStay != null)
		{
			OnStay(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(OnExit != null)
		{
			OnExit(other.gameObject);
		}
	}

}
