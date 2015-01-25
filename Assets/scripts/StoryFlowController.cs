using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryFlowController : MonoBehaviour {

	public GameObject[] photos;

	public AudioClip storySound;

	float frameDuration = 3f;

	// Use this for initialization
	void Start () {

		Camera.main.backgroundColor = Color.black;
		SoundManager.Get.PlayClip (storySound, true);
		photos [0].SetActive (false);
		photos [1].SetActive (false);
		photos [2].SetActive (false);
		photos [3].SetActive (false);

		StoryRoutine ();

	}

	void StoryRoutine(){

		this.ttAppend (frameDuration).ttAppend (delegate() {

						photos [0].SetActive (true);
			
				}).ttAppend (frameDuration, delegate() {
			
						photos [0].SetActive (false);
						photos [1].SetActive (true);
			
				}).ttAppend (frameDuration, delegate() {
			
						photos [1].SetActive (false);
						photos [2].SetActive (true);
			
				}).ttAppend (frameDuration, delegate() {
			
						photos [2].SetActive (false);
						photos [3].SetActive (true);
			
				}).ttAppend (frameDuration, delegate() {
				
						photos [2].SetActive (false);
						photos [3].SetActive (true);
				
				})
				.ttAppend (frameDuration, delegate() {
					
					Application.LoadLevel("main");

		});

	}
}
