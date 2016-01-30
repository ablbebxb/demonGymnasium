using UnityEngine;
using System.Collections;

public class Monster : Entity {

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	//handle selection in subclass to allow for later casting
	void OnMouseDown() {
		GameManager.manager.selectMonster (this);
	}

	public override void damage () {
		GameObject.Destroy (this.gameObject);
	}
}
