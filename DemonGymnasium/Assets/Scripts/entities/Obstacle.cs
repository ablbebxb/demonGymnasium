using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	new void Start() {
		base.Start ();
	}

	public override void damage ()
	{
		GameObject.Destroy (this.gameObject);
	}
}
