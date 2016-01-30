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
		//GetComponentInChildren<Renderer> ().material.color = Color.blue;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	public new void act() {
		base.act ();
        getCurrentTile().GetComponent<CleanTile>().cleanTile();
		Debug.Log ("Clean!!!!!");
	}

	public override void takeDamage() {
		Debug.Log ("OW!");
		HP--;
		if (HP <= 0) {
			//TODO death animation
			GameObject.Destroy(this.gameObject);
		}
	}
}
