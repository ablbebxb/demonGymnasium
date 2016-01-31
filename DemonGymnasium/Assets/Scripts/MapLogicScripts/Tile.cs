using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;
    public const int NEUTRAL = 2;

    GraphicTile graphicTile;
    int currentTileType = NEUTRAL;
	Entity entityPresent;
    int x;
    int y;
	Renderer rend;

	void Start() {
		rend = GetComponentInChildren<Renderer> ();
        graphicTile = GetComponent<GraphicTile>();
        currentTileType = NEUTRAL;
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
        if (entity.GetType() != typeof(Obstacle))
        {
            int type = (entity.getIsPlayer() ? 0 : 1);
            graphicTile = GetComponent<GraphicTile>();
            graphicTile.setAnim();
            setTileType(type);
        }
    }

    public void setTileType(int tileType)
    {
        graphicTile.selectTileType(tileType);
        this.currentTileType = tileType;
    }

    public int getCurrentTileType()
    {
        return currentTileType;
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
        
	}

	void OnMouseOver() {
		rend.material.color = Color.red;

	}

	void OnMouseExit() {
		rend.material.color = Color.white;
	}
    
}
