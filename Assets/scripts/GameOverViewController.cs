using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverViewController : MonoBehaviour {

	public Button quitButton;

	public Button startButton;

	// Use this for initialization
	void Start () {

		startButton.onClick.AddListener(delegate() {

			Application.LoadLevel("main");

		});

		quitButton.onClick.AddListener(delegate() {
			
			Application.LoadLevel("intro");
			
		});
			
	}
	

}
