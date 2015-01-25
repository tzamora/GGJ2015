using UnityEngine;
using System.Collections;

public class GameContext : MonoSingleton<GameContext> {

	public PlayerController player;

	public CarriageLeverController playerCarriage;

	public BackgroundScrollerController background;

	public GUIController GUI;

	// Use this for initialization
	void Awake () {
	
	}

}
