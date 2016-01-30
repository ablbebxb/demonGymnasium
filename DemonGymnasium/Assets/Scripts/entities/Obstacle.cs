using UnityEngine;
using System.Collections;

public class Obstacle : Entity {

	public override void damage ()
	{
		GameObject.Destroy (this.gameObject);
	}
}
