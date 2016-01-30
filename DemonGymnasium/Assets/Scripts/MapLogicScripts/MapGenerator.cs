using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int height = 10;
    public int width = 10;
    public GameObject tileObject;
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


			}
		}
	}

	public Tile getTileAtPosition(int x, int y) {
		return mapTiles [x, y];
	}

}
