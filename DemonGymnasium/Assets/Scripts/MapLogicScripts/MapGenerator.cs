using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int height = 10;
    public int width = 10;
    public GameObject tileObject;

	void Start() {
		generateMap ();
	}

	void generateMap() {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject obj = (GameObject)Instantiate(tileObject, Vector3.zero, new Quaternion());
				obj.GetComponent<Tile> ().setLocation (i, j);

			}
		}
	}


}
