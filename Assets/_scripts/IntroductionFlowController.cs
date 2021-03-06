using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionFlowController : MonoBehaviour {

	public AudioClip twinSound;

	public AudioClip photosSound;

	public AudioClip menuSound;

	public Text jamText;

	public GameObject[] photos;

	//public Image introScreen;

	public GameObject gameIntro;

	public Button startButton;

	public Button aboutButton;

	// Use this for initialization
	void Start () {

		IntroRoutine ();

		startButton.onClick.AddListener (delegate() {
		
			Application.LoadLevel("story");
		
		});

	}

	void IntroRoutine(){

		this.ttAppendLoop (0.5f, delegate(ttHandler handler) {
			
			Camera.main.backgroundColor = Color.Lerp (Color.white, Color.black, handler.t);
			
		}).ttAppend (2f).ttAppend (delegate(ttHandler handler) {
			
			SoundManager.Get.PlayClip (twinSound, false);
			
			jamText.gameObject.SetActive (true);
			
		})



		.ttAppend (2.5f).ttAppend (delegate() {
			
			jamText.gameObject.SetActive (false);

			SoundManager.Get.PlayClip (photosSound, false);
			
			photos [0].SetActive (true);
			
		}).ttAppend (0.2f, delegate() {
			
			photos [1].SetActive (true);
			
		}).ttAppend (0.2f, delegate() {
			
			photos [2].SetActive (true);
			
		}).ttAppend (0.2f, delegate() {
			
			photos [3].SetActive (true);
			
		})


		.ttAppend (1f, delegate() {
			photos [0].SetActive (false);
			photos [1].SetActive (false);
			photos [2].SetActive (false);
			photos [3].SetActive (false);

			photos [4].SetActive (true);
			photos [5].SetActive (true);
			photos [6].SetActive (true);
			photos [7].SetActive (true);
		})


		.ttAppend(2f).ttAppend(delegate(ttHandler handler){
			
			photos[4].SetActive(false);
			photos[5].SetActive(false);
			photos[6].SetActive(false);
			photos[7].SetActive(false);
			
			jamText.gameObject.SetActive(false);
			
			Camera.main.backgroundColor = Color.black;
			
					photos[8].SetActive(true);

			gameIntro.SetActive(true);
			
			SoundManager.Get.PlayClip(menuSound, true);
			
		});

	}
}
