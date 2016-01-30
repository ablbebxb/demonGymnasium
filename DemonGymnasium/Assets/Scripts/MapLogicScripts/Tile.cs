using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Entity entityPresent;
    int x;
    int y;
	Renderer rend;
	long lastClick;//track for double clicks

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
		Debug.Log ("Last: " + lastClick + " This: " + System.DateTime.Now.Ticks);
		if (System.DateTime.Now.Ticks - lastClick < 10000000) {
			if (entityPresent == null) {
				GameManager.manager.selectTile (this);
			} else if (entityPresent.GetType () == typeof(Player)) {
				GameManager.manager.selectPlayer ((Player)entityPresent);
			} else if (entityPresent.GetType () == typeof(Monster)) {
				GameManager.manager.selectMonster ((Monster)entityPresent);
			}
		}

		lastClick = System.DateTime.Now.Ticks;
	}

	void OnMouseOver() {
		rend.material.color = Color.red;
	}

	void OnMouseExit() {
		rend.material.color = Color.white;
	}
    
}
