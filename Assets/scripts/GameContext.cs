﻿using UnityEngine;
using System.Collections;

public class GameContext : MonoSingleton<GameContext> {

	public PlayerController player;

	public BackgroundScrollerController background;

	// Use this for initialization
	void Awake () {
	
	}

}
