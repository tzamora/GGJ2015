﻿using UnityEngine;
using System.Collections;

public class GameContext : MonoSingleton<GameContext> {

	public AudioClip gameSound;

	public PlayerController player;

	public CarriageLeverController playerCarriage;

	public BackgroundScrollerController background;

	public BackgroundScrollerController backgroundFore;

	public GUIController GUI;

	public SoundManager soundManager;

	// Use this for initialization
	void Awake () {
	
		SoundManager.Get.PlayClip (gameSound, true);

	}

}
