using UnityEngine;
using System.Collections;

public class Player : Entity {

	/*
	 * Default settings
	 */
	public int startingHP;

	private int HP;

	// Use this for initialization
	new void Start () {
		base.Start ();
		HP = startingHP;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	//handle selection in subclass to allow for later casting
	void OnMouseDown() {
		GameManager.manager.selectPlayer (this);
	}

	public override void damage() {
		HP--;
		if (HP <= 0) {
			//TODO death animation
			GameObject.Destroy(this.gameObject);
		}
	}
}
