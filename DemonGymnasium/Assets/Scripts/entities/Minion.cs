using UnityEngine;
using System.Collections;

public class Minion : Entity {

	// Use this for initialization
	new void Start () {
		base.Start ();
		GetComponentInChildren<Renderer> ().material.color = Color.yellow;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
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
