using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public float cameraMoveSpeed;
	public float cameraTesselationRate;
    public float cameraRotationSpeed;
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
            //mainCameraTransform.LookAt(new Vector3(generator.height/2, -5.05f, generator.width/2));
            mainCameraTransform.rotation = Quaternion.Slerp(mainCameraTransform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * cameraRotationSpeed);
            if ((targetPosition - mainCameraTransform.position).magnitude < cameraTesselationTermination) {
				float dist;
				if (isHumanTurn) {
                    //mainCameraTransform.eulerAngles += (playerCameraRotation - mainCameraTransform.eulerAngles) * 0.8f;
                    //print("Why am I not moving");
					dist = Quaternion.Angle(Quaternion.Euler(playerCameraRotation), mainCameraTransform.rotation);
				} else {
                    dist = Quaternion.Angle(Quaternion.Euler(monsterCameraRotation), mainCameraTransform.rotation);
				}
				if (dist < 2) {
					cameraInTransition = false;
				}
			}

		} else {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");

            Vector3 fwd = new Vector3(mainCameraTransform.forward.x, 0, mainCameraTransform.forward.z);
            Vector3 right = new Vector3(mainCameraTransform.right.x, 0, mainCameraTransform.right.z);
            Vector3 goalPosition = mainCameraTransform.position + (fwd * vInput + right * hInput) * 10;
            mainCameraTransform.position = Vector3.MoveTowards(mainCameraTransform.position, goalPosition, Time.deltaTime * cameraMoveSpeed);

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
