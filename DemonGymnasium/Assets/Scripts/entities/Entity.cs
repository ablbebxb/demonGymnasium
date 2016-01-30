using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	/**
	 * Directions!!
	 * North is Z+
	 * South is Z-
	 * East is x+
	 * West is x-
	 * 
	 * Tile size is 1
	 */


	/*
	 * Default settings
	 */
	public int startingActionPoints;
	public bool m;
	public Vector3 direction;

	private int actionPoints;
	private bool isMoving;//true while the entity is moving from one space to another
	private Vector3 target;//the target space the entity is moving to

	// Use this for initialization
	void Start () {
		actionPoints = startingActionPoints;
		isMoving = false;
		target = Vector3.zero;
	}
	
	// Update is called once per frame
	public void Update () {
		if (isMoving) {
			this.transform.position += (target - this.transform.position).normalized * 0.1f;

			//check target reached
			if ((target - this.transform.position).magnitude < 0.1) {
				isMoving = false;
			}
		}

		if (m) {
			move(direction);
			m = false;
		}
	}

	public abstract void damage ();

	private void move(Vector3 dir) {
		Debug.Log("Move");
		if (isMoving) {
			return;
		}
		isMoving = true;
		target = this.transform.position + dir;
	}

	public void moveNorth () {
		move(new Vector3 (0, 0, 1));
	}

	public void moveSouth () {
		move(new Vector3 (0, 0, -1));
	}

	public void moveEast () {
		move(new Vector3 (1, 0, 0));
	}

	public void moveWest () {
		move(new Vector3 (-1, 0, 0));
	}
}
