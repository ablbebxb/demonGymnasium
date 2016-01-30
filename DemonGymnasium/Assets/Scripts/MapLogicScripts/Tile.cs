using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Entity entityPresent;
    int x;
    int y;
	Renderer rend;

	void Start() {
		rend = GetComponentInChildren<Renderer> ();
	}

    public bool getIsObstructed()
    {
		return entityPresent != null;
    }

	public Entity getCurrentEntity() {
		return this.entityPresent;
	}

	public void setObstruction(Entity entity) {
        removeEntity();
        entity.transform.parent = this.transform.parent;
        entity.transform.position = transform.position;
		this.entityPresent = entity;
	}

    public void removeEntity()
    {
        if (entityPresent == null)
        {
            return;
        }
        this.entityPresent.transform.parent = null;
        this.entityPresent = null;
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

	void OnMouseDown() {
		GameManager.manager.selectTile (this);
	}

	void OnMouseOver() {
		rend.material.color = Color.red;
	}

	void OnMouseExit() {
		rend.material.color = Color.white;
	}
    
}
