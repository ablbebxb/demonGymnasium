using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	public float cameraMoveSpeed;
	public float cameraTesselationRate;
    public float cameraRotationSpeed;
	public float cameraTesselationTermination;
	public int actionsPerTurn;

	public int monsterRange;
    public GameObject playerModal;

	public Vector3 playerCameraPosition, playerCameraRotation, monsterCameraPosition, monsterCameraRotation;//initial camera positioning per side
	public float playerSpriteRotation, monsterSpriteRotation;//the static y-component of the sprite rotations

	private bool isHumanTurn;
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;
	private MapGenerator generator;
	private int actionCounter;
	private bool cameraInTransition;
	private Vector3 targetPosition, targetRotation;
    private int state;//0- player select, 1- move, 2- shoot

	// Use this for initialization
	void Start () {
		GameManager.manager = this;
		isHumanTurn = true;
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		mainCameraTransform.position = playerCameraPosition;
		mainCameraTransform.eulerAngles = playerCameraRotation;
		generator = GetComponent<MapGenerator> ();
		actionCounter = 0;
        state = 0;

        
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

    public int getState()
    {
        return state;
    }

    public void setupAttack()
    {
        if (actionsPerTurn - actionCounter > 2)
        {
            state = 2;
            playerModal.SetActive(false);
            //TODO highlight possible targets
        }
    }

    public void setupMove()
    {
        state = 1;
        playerModal.SetActive(false);
        //TODO highlight possible moves
    }

    public void expand()
    {
        if (selectedObject != null)
        {
            int x = selectedObject.getCurrentTile().getX();
            int y = selectedObject.getCurrentTile().getY();
            int type = (isHumanTurn ? 0 : 1);

            if (x > 0 && !isObstacle(getEntityAtPosition(x - 1, y)))
            {
                damageIfEnemy(getEntityAtPosition(x - 1, y));
                generator.getTileAtPosition(x - 1, y).setTileType(type);
            }

            if (x < generator.width - 1 && !isObstacle(getEntityAtPosition(x + 1, y)))
            {
                damageIfEnemy(getEntityAtPosition(x + 1, y));
                generator.getTileAtPosition(x + 1, y).setTileType(type);
            }

            if (y > 0 && !isObstacle(getEntityAtPosition(x, y - 1)))
            {
                damageIfEnemy(getEntityAtPosition(x, y - 1));
                generator.getTileAtPosition(x, y - 1).setTileType(type);
            }

            if (y < generator.height - 1 && !isObstacle(getEntityAtPosition(x, y + 1)))
            {
                damageIfEnemy(getEntityAtPosition(x, y + 1));
                generator.getTileAtPosition(x, y + 1).setTileType(type);
            }
        }

        recordAction();
        state = 0;
    }

	public Vector3 getSpriteRotation() {
		if (isHumanTurn) {
			return new Vector3(360 - mainCameraTransform.eulerAngles.x, playerSpriteRotation, 0);
		} else {
			return new Vector3(mainCameraTransform.eulerAngles.x, monsterSpriteRotation, 0);
		}
	}

	public void selectPlayer(Entity player) {
        if (state == 0)
        {
            if (isHumanTurn == player.getIsPlayer())
            {
                selectedObject = player;
                playerModal.SetActive(true);
            }
        }
        
	}

	public void selectTile(Tile tile) {
        if (selectedObject != null && tile.getCurrentEntity() == selectedObject)
        {
            state = 0;
            selectPlayer(tile.getCurrentEntity());
            return;
        }

        if (state == 1)
        {
            if (selectedObject != null && tile.getCurrentEntity() == null)
            {
                int x = tile.getX();
                int y = tile.getY();

                Tile sourceTile = selectedObject.getCurrentTile();
                bool acted = moveTo(selectedObject, sourceTile.getX(), sourceTile.getY(), x, y);
                /*if (canMoveTo(sourceTile.getX(), sourceTile.getY(), x, y))
                {
                    int type = (isHumanTurn ? 0 : 1);
                    if (x > 0 && getEntityAtPosition(x - 1, y) == selectedObject)
                    {
                        moveFromPositionToPosition(x - 1, y, x, y);
                        acted = selectedObject.moveEast();
                    }
                    else if (x < generator.width - 1 && getEntityAtPosition(x + 1, y) == selectedObject)
                    {
                        moveFromPositionToPosition(x + 1, y, x, y);
                        acted = selectedObject.moveWest();
                    }
                    else if (y > 0 && getEntityAtPosition(x, y - 1) == selectedObject)
                    {
                        moveFromPositionToPosition(x, y - 1, x, y);
                        acted = selectedObject.moveNorth();
                    }
                    else if (y < generator.height - 1 && getEntityAtPosition(x, y + 1) == selectedObject)
                    {
                        moveFromPositionToPosition(x, y + 1, x, y);
                        acted = selectedObject.moveSouth();
                    }

                    if (selectedObject.GetType() == typeof(King))
                    {
                        if (y < generator.height - 1 && x < generator.width - 1 && getEntityAtPosition(x + 1, y + 1) == selectedObject)
                        {
                            moveFromPositionToPosition(x + 1, y + 1, x, y);
                            acted = ((King)selectedObject).moveSouthWest();
                        }
                        else if (y < generator.height - 1 && x > 0 && getEntityAtPosition(x - 1, y + 1) == selectedObject)
                        {
                            moveFromPositionToPosition(x - 1, y + 1, x, y);
                            acted = ((King)selectedObject).moveSouthEast();
                        }
                        else if (y > 0 && x < generator.width - 1 && getEntityAtPosition(x + 1, y - 1) == selectedObject)
                        {
                            moveFromPositionToPosition(x + 1, y - 1, x, y);
                            acted = ((King)selectedObject).moveNorthWest();
                        }
                        else if (y > 0 && x > 0 && getEntityAtPosition(x - 1, y - 1) == selectedObject)
                        {
                            moveFromPositionToPosition(x - 1, y - 1, x, y);
                            acted = ((King)selectedObject).moveNorthEast();
                        }
                    }*/

                    if (acted)
                    {
                        recordAction();
                    }
                //}
            }
        } else if (state == 2) {
            if (selectedObject != null)
            {
                Entity source = (Entity)selectedObject;
                int sourceX = selectedObject.getCurrentTile().getX();
                int sourceY = selectedObject.getCurrentTile().getY();

                int x = tile.getX();
                int y = tile.getY();

                if (checkLineofSight(sourceX, sourceY, x, y))
                {
                    source.act();
                    if (tile.getCurrentEntity() != null && tile.getCurrentEntity().GetType() != typeof(Obstacle))
                    {
                        damageIfEnemy(tile.getCurrentEntity());
                    }

                    int type = (isHumanTurn ? 0 : 1);

                    if (sourceX == x && sourceY < y)
                    {
                        for (int i = 1; i <= y - sourceY; i++)
                        {
                            generator.getTileAtPosition(x, sourceY + i).setTileType(type);
                        }
                    }
                    else if (sourceX == x && sourceY > y)
                    {
                        for (int i = 0; i < sourceY - y; i++)
                        {
                            generator.getTileAtPosition(x, y + i).setTileType(type);
                        }
                    }
                    else if (sourceY == y && sourceX < x)
                    {
                        for (int i = 1; i <= x - sourceX; i++)
                        {
                            generator.getTileAtPosition(sourceX + i, y).setTileType(type);
                        }
                    }
                    else if (sourceY == y && sourceX > x)
                    {
                        for (int i = 0; i < sourceX - x; i++)
                        {
                            generator.getTileAtPosition(x + i, y).setTileType(type);
                        }
                    }

                    recordAction();
                    recordAction();

                    state = 0;
                }
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

    private bool moveTo(Entity entity, int sourceX, int sourceY, int x, int y)
    {
        bool obstructed = false;
        int type = (isHumanTurn ? 0 : 1);
        if (sourceX == x && sourceY < y)
        {
            for (int i = 1; i < y - sourceY; i++)
            {
                Tile tile = generator.getTileAtPosition(x, sourceY + i);
                if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                entity.moveNorth(y - sourceY);
            }
        }
        else if (sourceX == x && sourceY > y)
        {
            for (int i = 1; i < sourceY - y; i++)
            {
                Tile tile = generator.getTileAtPosition(x, y + i);
                if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                entity.moveSouth(sourceY - y);
            }
        }
        else if (sourceY == y && sourceX < x)
        {
            for (int i = 1; i < x - sourceX; i++)
            {
                Tile tile = generator.getTileAtPosition(sourceX + i, y);
                if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                entity.moveEast(x - sourceX);
            }
        }
        else if (sourceY == y && sourceX > x)
        {
            for (int i = 1; i < sourceX - x; i++)
            {
                Tile tile = generator.getTileAtPosition(x + i, y);
                if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                {
                    obstructed = true;
                }
            }

            if (!obstructed)
            {
                entity.moveWest(sourceX - x);
            }
        } else if (entity.GetType() == typeof(King) && Mathf.Abs(sourceX - x) == Mathf.Abs(sourceY - y))
        {
            King king = (King)entity;
            if (sourceY > y && sourceX > x)
            {
                for (int i = 1; i < sourceX - x; i++)
                {
                    Tile tile = generator.getTileAtPosition(x + i, y + i);
                    if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthWest(sourceX - x);
                }
            }
            else if (sourceY < y && sourceX > x)
            {
                for (int i = 1; i < sourceX - x; i++)
                {
                    Tile tile = generator.getTileAtPosition(x + i, y - i);
                    if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthWest(sourceX - x);
                }
            }
            else if (sourceY > y && sourceX < x)
            {
                for (int i = 1; i < x - sourceX; i++)
                {
                    Tile tile = generator.getTileAtPosition(x - i, y + i);
                    if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveSouthEast(x - sourceX);
                }
            }
            else if (sourceY < y && sourceX < x)
            {
                for (int i = 1; i < x - sourceX; i++)
                {
                    Tile tile = generator.getTileAtPosition(x - i, y - i);
                    if (tile.getIsObstructed() || tile.getCurrentTileType() != type)
                    {
                        obstructed = true;
                    }
                }

                if (!obstructed)
                {
                    king.moveNorthEast(x - sourceX);
                }
            }
        }
        else
        {
            obstructed = true;
        }

        if (!obstructed)
        {
            moveFromPositionToPosition(sourceX, sourceY, x, y);
        }

        return !obstructed;
    }
	
	private void recordAction() {
		actionCounter++;
		if (isHumanTurn && actionCounter >= actionsPerTurn) {
			changeTurn();
		} else if (!isHumanTurn && actionCounter >= actionsPerTurn) {
			changeTurn();
		}
	}

	private void changeTurn() {
		isHumanTurn = !isHumanTurn;
		actionCounter = 0;
        state = 0;
		selectedObject = null;
        playerModal.SetActive(false);

		foreach (Entity monster in generator.getMonsters()) {
			monster.reset();
		}

		foreach (Entity player in generator.getPlayers()) {
			player.reset();
		}

		if (isHumanTurn) {
			moveCameraToPlayer();
		} else {
			moveCameraToMonster();
		}
        MapGenerator.updateTileScore();
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

    private void damageIfEnemy(Entity other)
    {
        if (other != null && other.getIsPlayer() != isHumanTurn)
        {
            other.takeDamage();
        }
    }
	
    private bool isObstacle(Entity obj)
    {
        if (obj != null && obj.GetType() == typeof(Obstacle))
        {
            return true;
        }
        return false;
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
