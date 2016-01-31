using UnityEngine;
using System.Collections;

public class King : Entity {

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

	public override void takeDamage() {
		Debug.Log ("OW!");
		HP--;
		if (HP <= 0) {
			//TODO death animation
			GameObject.Destroy(this.gameObject);
		}
	}

    public bool moveNorthEast()
    {
        return base.move(new Vector3(1, 0, 1));
    }

    public bool moveSouthEast()
    {
        return base.move(new Vector3(1, 0, -1));
    }

    public bool moveNorthWest()
    {
        return base.move(new Vector3(-1, 0, 1));
    }

    public bool moveSouthWest()
    {
        return base.move(new Vector3(-1, 0, -1));
    }
}
