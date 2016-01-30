using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	new void Start() {
		base.Start ();
	}

    new void Update()
    {

    }

	public override void damage ()
	{
		GameObject.Destroy (this.gameObject);
	}
}
