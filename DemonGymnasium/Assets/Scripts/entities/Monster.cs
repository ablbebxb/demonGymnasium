using UnityEngine;
using System.Collections;

public class Monster : Entity {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	public override void damage () {
		GameObject.Destroy (this.gameObject);
	}
}
