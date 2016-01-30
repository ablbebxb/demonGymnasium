using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Entity entityPresent;
    int x;
    int y;
    bool isObstructed;

    public bool getIsObstructed()
    {
		return entityPresent != null;
    }

	public Entity getCurrentEntity() {
		return this.entityPresent;
	}

	public void setObstruction(Entity entity) {
		this.entityPresent = entity;
	}



    public void setLocation(int x, int y)
    {
        this.x = x;
        this.y = y;
		transform.position = new Vector3 (x, 0, y);
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
    
}
