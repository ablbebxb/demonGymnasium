using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    public int height = 10;
    public int width = 10;
    public float obstructionPercantage = .05f;
	public GameObject tileObject;
	public Entity obstructionObject;
	public Entity monsterObject;
	public Entity playerObject;
	public int numMonsters;
	public int numPlayers;
	Tile[,] mapTiles;

	private Entity[] monsters;
	private Entity[] players;

	void Start() {
		mapTiles = new Tile[width, height];
		monsters = new Entity[numMonsters];
		players = new Entity[numPlayers];
		generateMap();
	}

	void generateMap() {
		int monsterCount = 0, playerCount = 0;
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				GameObject obj = (GameObject)Instantiate(tileObject, Vector3.zero, new Quaternion());
				Tile tile = obj.GetComponent<Tile> ();

				tile.setLocation (i, j);
                tile.transform.parent = this.transform;
				mapTiles [i, j] = tile;

                if (Random.Range(0f, 1f)  < obstructionPercantage)
                {
					/*Entity entity = ((GameObject)Instantiate(obstructionObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
                    tile.setInitialEntity(entity);*/
                }

				//place monsters in first column and players in last
				if (monsterCount < numMonsters && i == 0 && !tile.getIsObstructed()) {
					Entity entity = ((GameObject)Instantiate(monsterObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
                    tile.setInitialEntity(entity);
					monsters[monsterCount] = entity;
					monsterCount++;
				} else if (playerCount < numPlayers && i == width - 1 && !tile.getIsObstructed()) {
					Entity entity = ((GameObject)Instantiate(playerObject.gameObject, Vector3.zero, new Quaternion())).GetComponent<Entity>();
					tile.setInitialEntity(entity);
					players[playerCount] = entity;
					playerCount++;
				}

			}
		}
	}

	public Tile getTileAtPosition(int x, int y) {
		return mapTiles [x, y];
	}

	public Entity[] getMonsters() {
		return monsters;
	}

	public Entity[] getPlayers() {
		return players;
	}

}
