using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public GameObject playerPrefab;//tmp, eventually players should proabably be added in the  generator

	private bool isHumanTurn;//true- human, false- monster
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;
	private MapGenerator generator;//TODO replace with map manager eventually

	// Use this for initialization
	void Start () {
		GameManager.manager = this;
		isHumanTurn = true;
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		generator = GetComponent<MapGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void changeTurn() {
		isHumanTurn = !isHumanTurn;
		selectedObject = null;
	}

	public void selectPlayer(Player player) {
		generator.getTileAtPosition (0, 0).setObstruction (playerPrefab.GetComponent<Player>());

		if (isHumanTurn) {
			selectedObject = player;
			Debug.Log ("Select player");
		}
	}

	public void selectMonster(Monster monster) {
		if (!isHumanTurn) {
			selectedObject = monster;
		}
	}

	public void selectTile(Tile tile) {
		Debug.Log ("Select Tile");

		if (tile.getCurrentEntity ().GetComponent<Entity>() != null) {
			if (isHumanTurn && tile.getCurrentEntity().GetComponent<Player>() != null) {
				selectPlayer(tile.getCurrentEntity().GetComponent<Player>());
			} else if (!isHumanTurn && tile.getCurrentEntity().GetComponent<Monster>() != null) {
				selectMonster(tile.getCurrentEntity().GetComponent<Monster>());
			}
		} else if (selectedObject != null) {
			int x = tile.getX ();
			int y = tile.getY ();
			if (x > 0 && getEntityAtPosition (tile.getX () - 1, tile.getY ()) == selectedObject) {
				moveToPositionFromPosition (x - 1, y, x, y);
				selectedObject.moveEast ();
			} else if (x < generator.width && getEntityAtPosition (tile.getX () + 1, tile.getY ()) == selectedObject) {
				moveToPositionFromPosition (x + 1, y, x, y);
				selectedObject.moveWest ();
			} else if (y > 0 && getEntityAtPosition (tile.getX (), tile.getY () - 1) == selectedObject) {
				moveToPositionFromPosition (x, y - 1, x, y);
				selectedObject.moveNorth ();
			} else if (y < generator.height && getEntityAtPosition (tile.getX (), tile.getY () + 1) == selectedObject) {
				moveToPositionFromPosition (x, y + 1, x, y);
				selectedObject.moveSouth ();
			}
		}
	}

	public Transform getCamera() {
		return mainCameraTransform;
	}

	//TODO put inside map generator/ map manager
	private Entity getEntityAtPosition(int x, int y) {
		return generator.getTileAtPosition (x, y).getCurrentEntity ();
	}

	private void moveToPositionFromPosition(int x1, int y1, int x2, int y2) {
		generator.getTileAtPosition (x1, y1).setObstruction(null);
		generator.getTileAtPosition (x2, y2).setObstruction(selectedObject);
	}
}
