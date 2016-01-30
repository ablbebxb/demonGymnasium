using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public float cameraMoveSpeed;

	private bool isHumanTurn;
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;
	private MapGenerator generator;

	// Use this for initialization
	void Start () {
		GameManager.manager = this;
		isHumanTurn = true;
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		generator = GetComponent<MapGenerator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//camera controls
		if (Input.GetKey (KeyCode.UpArrow)) {
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z + cameraMoveSpeed);
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, mainCameraTransform.position.z - cameraMoveSpeed);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x + cameraMoveSpeed, mainCameraTransform.position.y, mainCameraTransform.position.z);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			mainCameraTransform.position = new Vector3(mainCameraTransform.position.x - cameraMoveSpeed, mainCameraTransform.position.y, mainCameraTransform.position.z);
		}
	}

	private void changeTurn() {
		isHumanTurn = !isHumanTurn;
		selectedObject.resetActions();
		selectedObject = null;
	}

	public void selectPlayer(Player player) {
		if (isHumanTurn) {
			selectedObject = player;
		}
	}

	public void selectMonster(Monster monster) {
		if (!isHumanTurn) {
			selectedObject = monster;
		}
	}

	public void selectTile(Tile tile) {
		if (selectedObject != null) {
			int x = tile.getX ();
			int y = tile.getY ();
			bool movesLeft = true;
			if (x > 0 && getEntityAtPosition (tile.getX () - 1, tile.getY ()) == selectedObject) {
				moveToPositionFromPosition (x - 1, y, x, y);
				movesLeft = selectedObject.moveEast ();
			} else if (x < generator.width - 1 && getEntityAtPosition (tile.getX () + 1, tile.getY ()) == selectedObject) {
				moveToPositionFromPosition (x + 1, y, x, y);
				movesLeft = selectedObject.moveWest ();
			} else if (y > 0 && getEntityAtPosition (tile.getX (), tile.getY () - 1) == selectedObject) {
				moveToPositionFromPosition (x, y - 1, x, y);
				movesLeft = selectedObject.moveNorth ();
			} else if (y < generator.height - 1 && getEntityAtPosition (tile.getX (), tile.getY () + 1) == selectedObject) {
				moveToPositionFromPosition (x, y + 1, x, y);
				movesLeft = selectedObject.moveSouth ();
			}

			if (!movesLeft) {
				changeTurn();
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
		generator.getTileAtPosition (x1, y1).setEntity(null);
		generator.getTileAtPosition (x2, y2).setEntity(selectedObject);
	}
}
