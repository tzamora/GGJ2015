using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

	public Text counterText;

	public GameObject GameOverView;

	public List<Image> player1Health;

	public List<Image> player2Health;

	public void ShowGameOver(){

		GameOverView.SetActive(true);

	}
}
