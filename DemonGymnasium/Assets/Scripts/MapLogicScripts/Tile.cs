using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;
    public const int NEUTRAL = 2;
    public Color janitorColor = Color.blue;
    public Color demonColor = Color.green;

    GraphicTile graphicTile;
    int currentTileType = NEUTRAL;
	Entity entityPresent;
    int x;
    int y;
	Renderer[] rend;

	public Material[] floorMaterials = new Material[5];

	void Start() {
		rend = GetComponentsInChildren<Renderer> ();
        

        

        graphicTile = GetComponent<GraphicTile>();
		PickRandomMaterialForNeutral ();
        if (entityPresent)
        {
            currentTileType = entityPresent.entityType;
        }
        else
        {
            currentTileType = 2;
        }
        
    }

	void PickRandomMaterialForNeutral(){
		int randomNum = (int)Random.Range(0, 100);
		int resultIndex;
		if (randomNum >= 0 && randomNum < 60) {
			resultIndex = 0;
		} 
		else if (randomNum >= 60 && randomNum < 65) {
			resultIndex = 1;
		} 
		else if (randomNum >= 65 && randomNum < 90) {
			resultIndex = 2;
		} 
		else if (randomNum >= 90 && randomNum < 95) {
			resultIndex = 3;
		} 
		else {
			resultIndex = 4;
		}


        foreach (Renderer r in rend)
        {
            r.materials = new Material[] { floorMaterials[resultIndex] };
        }
        rend[0].material.color = janitorColor;
        rend[1].material.color = demonColor;
        rend[0].gameObject.SetActive(false);
        rend[1].gameObject.SetActive(false);
        Debug.Log (resultIndex);
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
        foreach(Renderer r in rend)
        {
            if (r.enabled)
            {
                r.material.color = Color.red;
            }
        }

	}

	void OnMouseExit() {
        foreach (Renderer r in rend)
        {
            if (r.enabled)
            {
                r.material.color = Color.white;
            }
        }
    }
    
}
