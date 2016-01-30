using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	new void Start() {
		base.Start();
		GetComponentInChildren<Renderer> ().material.color = Color.black;
	}

    new void Update()
    {
		base.Update();
    }

	public override void damage ()
	{
		GameObject.Destroy (this.gameObject);
	}
}
