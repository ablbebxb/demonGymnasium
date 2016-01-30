using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public float cameraMoveSpeed;
	public float cameraTesselationRate;
	public float cameraTesselationTermination;
	public int monsterActionsPerTurn;
	public int playerActionsPerTurn;

	public int monsterRange;//TODO move to monster class? Might make sense here though

	public Vector3 playerCameraPosition, playerCameraRotation, monsterCameraPosition, monsterCameraRotation;//initial camera positioning per side
	public float playerSpriteRotation, monsterSpriteRotation;//the static y-component of the sprite rotations

	private bool isHumanTurn;
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;
	private MapGenerator generator;
	private int actionCounter;
	private bool cameraInTransition;
	private Vector3 targetPosition, targetRotation;

	// Use this for initialization
	void Start () {
		GameManager.manager = this;
		isHumanTurn = true;
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		mainCameraTransform.position = playerCameraPosition;
		mainCameraTransform.eulerAngles = playerCameraRotation;
		generator = GetComponent<MapGenerator> ();
		actionCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraInTransition) {
			mainCameraTransform.position += (targetPosition - mainCameraTransform.position) * cameraTesselationRate;
			mainCameraTransform.LookAt(new Vector3(generator.height/2, -5.05f, generator.width/2));

			if((targetPosition - mainCameraTransform.position).magnitude < cameraTesselationTermination) {
				float dist;
				if (isHumanTurn) {
					//mainCameraTransform.eulerAngles += (playerCameraRotation - mainCameraTransform.eulerAngles) * 0.8f;
					mainCameraTransform.rotation = Quaternion.Slerp(mainCameraTransform.rotation, Quaternion.Euler(playerCameraRotation), Time.deltaTime * 1.0f);
					dist = Quaternion.Angle(Quaternion.Euler(playerCameraRotation), mainCameraTransform.rotation);
				} else {
					mainCameraTransform.eulerAngles += (monsterCameraRotation - mainCameraTransform.eulerAngles) * 0.8f;
					dist = (monsterCameraRotation - mainCameraTransform.eulerAngles).magnitude;
				}
				if (dist < 2) {
					cameraInTransition = false;
				}
			}

		} else {
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
	}

	public Vector3 getSpriteRotation() {
		if (isHumanTurn) {
			return new Vector3(360 - mainCameraTransform.eulerAngles.x, playerSpriteRotation, 0);
		} else {
			return new Vector3(mainCameraTransform.eulerAngles.x, monsterSpriteRotation, 0);
		}
	}


	public void selectPlayer(Player player) {
		if (isHumanTurn) {
			if (!player.getHasActed() && selectedObject == player) {
				player.act();
				recordAction ();
			} else {
				selectedObject = player;
			}
		} else if (selectedObject != null) { //if the monster clicks on a player, make sure he has a monster selected before trying to attack
			Monster monster = (Monster)selectedObject;
			int sourceX = selectedObject.getCurrentTile().getX();
			int sourceY = selectedObject.getCurrentTile().getY();
			
			Debug.Log ("Try to hit");
			
			if (!monster.getHasActed() && checkLineofSight(sourceX, sourceY, player.getCurrentTile().getX(), player.getCurrentTile().getY())) {
				monster.act();
				player.takeDamage();
				recordAction();
			}
		}
	}

	public void selectMonster(Monster monster) {
		if (!isHumanTurn) {
			selectedObject = monster;
		} else if (selectedObject != null) { //if the player clicks on a monster, make sure he has a player selected before trying to attack
			Entity player = selectedObject;//Call stub, not cleaning method in player class
			int sourceX = selectedObject.getCurrentTile().getX();
			int sourceY = selectedObject.getCurrentTile().getY();
			
			Debug.Log ("Try to hit");
			
			if (!monster.getHasActed() && checkLineofSight(sourceX, sourceY, player.getCurrentTile().getX(), player.getCurrentTile().getY())) {
				player.act();
				monster.takeDamage();
				recordAction();
			}
		}
	}

	public void selectTile(Tile tile) {
		if (selectedObject != null) {
			int x = tile.getX ();
			int y = tile.getY ();
			bool acted = false;
			Entity entityOnTile = getEntityAtPosition(x, y);
			if (x > 0 && getEntityAtPosition (x - 1, y) == selectedObject) {
				moveFromPositionToPosition (x - 1, y, x, y);
				acted = selectedObject.moveEast ();
			} else if (x < generator.width - 1 && getEntityAtPosition (x + 1, y) == selectedObject) {
				moveFromPositionToPosition (x + 1, y, x, y);
				acted = selectedObject.moveWest ();
			} else if (y > 0 && getEntityAtPosition (x, y - 1) == selectedObject) {
				moveFromPositionToPosition (x, y - 1, x, y);
				acted = selectedObject.moveNorth ();
			} else if (y < generator.height - 1 && getEntityAtPosition (x, y + 1) == selectedObject) {
				moveFromPositionToPosition (x, y + 1, x, y);
				acted = selectedObject.moveSouth ();
			}

			if (acted) {
				recordAction();
			}
		}
	}

	public Transform getCamera() {
		return mainCameraTransform;
	}

	private bool checkLineofSight(int sourceX, int sourceY, int x, int y) {
		bool lineOfSight = true;
		if (sourceX == x && sourceY < y && y - sourceY <= monsterRange) {
			for (int i = 1; i < y - sourceY; i++) {
				if (generator.getTileAtPosition (x, sourceY + i).getIsObstructed ()) {
					lineOfSight = false;
				}
			}
		} else if (sourceX == x && sourceY > y && sourceY - y <= monsterRange) {
			for (int i = 1; i < sourceY - y; i++) {
				if (generator.getTileAtPosition (x, y + i).getIsObstructed ()) {
					lineOfSight = false;
				}
			}
		} else if (sourceY == y && sourceX < x && x - sourceX <= monsterRange) {
			for (int i = 1; i < x - sourceX; i++) {
				if (generator.getTileAtPosition (sourceX + i, y).getIsObstructed ()) {
					lineOfSight = false;
				}
			}
		} else if (sourceY == y && sourceX > x && sourceX - x <= monsterRange) {
			for (int i = 1; i < sourceX - x; i++) {
				if (generator.getTileAtPosition (x + i, y).getIsObstructed ()) {
					lineOfSight = false;
				}
			}
		} else {
			lineOfSight = false;
		}
		return lineOfSight;
	}
	
	private void recordAction() {
		actionCounter++;
		if (isHumanTurn && actionCounter >= playerActionsPerTurn) {
			changeTurn();
		} else if (!isHumanTurn && actionCounter >= monsterActionsPerTurn) {
			changeTurn();
		}
	}

	private void changeTurn() {
		isHumanTurn = !isHumanTurn;
		actionCounter = 0;
		selectedObject = null;

		foreach (Monster monster in generator.getMonsters()) {
			monster.reset();
		}

		foreach (Player player in generator.getPlayers()) {
			player.reset();
		}

		if (isHumanTurn) {
			moveCameraToPlayer();
		} else {
			moveCameraToMonster();
		}
	}

	/**
	 * Move the camera to the initial position and rotation that should be used for the player's turn
	 */
	private void moveCameraToPlayer() {
		targetPosition = playerCameraPosition;
		targetRotation = playerCameraRotation;
		cameraInTransition = true;
	}

	/**
	 * Move the camera to the initial position and rotation that should be used for the monster's turn
	 */
	private void moveCameraToMonster() {
		targetPosition = monsterCameraPosition;
		targetRotation = monsterCameraRotation;
		cameraInTransition = true;
	}

	
	//TODO put inside map generator/ map manager
	private Entity getEntityAtPosition(int x, int y) {
		return generator.getTileAtPosition (x, y).getCurrentEntity ();
	}

	private void moveFromPositionToPosition(int x1, int y1, int x2, int y2) {
		generator.getTileAtPosition (x1, y1).setEntity(null);
		Tile targetTile = generator.getTileAtPosition (x2, y2);
		targetTile.setEntity(selectedObject);
		selectedObject.setCurrentTile (targetTile);
	}
}
