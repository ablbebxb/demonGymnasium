﻿using UnityEngine;
using System.Collections;

public class Minion : Entity {
    Animator anim;

	// Use this for initialization
	new void Start () {
		base.Start ();
        anim = GetComponent<Animator>();
		GetComponentInChildren<Renderer> ().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
        if (anim != null)
        {
            anim.SetBool("isMoving", getIsMoving());
        }
	}

	public new void act() {
		base.act ();
		//TODO attack animation
		Debug.Log ("Attack!");
	}

	public override void takeDamage () {
		GameObject.Destroy (this.gameObject);
	}
}
