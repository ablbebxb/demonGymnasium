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

	public void setEntity(Entity entity) {
        removeEntity();
		this.entityPresent = entity;
	}

	public void setInitialEntity(Entity entity) {
        removeEntity();
        entity.transform.parent = this.transform.parent;
        entity.transform.position = transform.position;
		this.entityPresent = entity;
		entity.setCurrentTile (this);
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
		if (entityPresent == null) {
			GameManager.manager.selectTile (this);
		} else if (entityPresent.GetType () == typeof(King)) {
			GameManager.manager.selectPlayer ((King)entityPresent);
		} else if (entityPresent.GetType () == typeof(Minion)) {
			GameManager.manager.selectMonster ((Minion)entityPresent);
		}
	}

	void OnMouseOver() {
		rend.material.color = Color.red;
	}

	void OnMouseExit() {
		rend.material.color = Color.white;
	}
    
}
