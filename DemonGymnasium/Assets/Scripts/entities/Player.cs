﻿using UnityEngine;
using System.Collections;

public class Player : Entity {

	/*
	 * Default settings
	 */
	public int startingHP;

	private int HP;

	// Use this for initialization
	void Start () {
		HP = startingHP;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void damage() {
		HP--;
		if (HP <= 0) {
			//TODO death animation
			GameObject.Destroy(this.gameObject);
		}
	}
}
