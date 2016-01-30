using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int height = 10;
    public int width = 10;
    public float obstructionPercantage = .05f;
    public GameObject tileObject;
    public Entity obstructionObject;
	Tile[,] mapTiles;

	void Start() {
		mapTiles = new Tile[width, height];
		generateMap ();
	}

	void generateMap() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject obj = (GameObject)Instantiate(tileObject, Vector3.zero, new Quaternion());
				Tile tile = obj.GetComponent<Tile> ();
				tile.setLocation (i, j);
                tile.transform.parent = this.transform;
				mapTiles [i, j] = tile;
                if (Random.Range(0f, 1f)  < obstructionPercantage)
                {
                    Entity entity = ((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>(); ;
                    tile.setObstruction(entity);
                }


			}
		}
	}

	public Tile getTileAtPosition(int x, int y) {
		return mapTiles [x, y];
	}

}
