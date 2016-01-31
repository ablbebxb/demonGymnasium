using UnityEngine;
using System.Collections;

public class King : Entity {
	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

    public bool moveNorthEast(int units)
    {
        return base.move(new Vector3(1, 0, 1), units);
    }

    public bool moveSouthEast(int units)
    {
        return base.move(new Vector3(1, 0, -1), units);
    }

    public bool moveNorthWest(int units)
    {
        return base.move(new Vector3(-1, 0, 1), units);
    }

    public bool moveSouthWest(int units)
    {
        return base.move(new Vector3(-1, 0, -1), units);
    }
}
