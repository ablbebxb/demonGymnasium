using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;

    public int turnsPerPlayer;

    public int currentTurn;
    public LinkedList<Entity> currentPlayers;
    public bool gameInProgress = true;
    public static GameManager gameManager;

    int turnsLeft;
    int winner = -1;
    CameraManager cameraManager;
    UIManager uiManager;


    void Start()
    {
        gameManager = this;
        currentPlayers = new LinkedList<Entity>();
        currentTurn = JANITOR;
        cameraManager = GetComponent<CameraManager>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        turnsLeft = turnsPerPlayer;
    }

    public bool getPlayerTurn()
    {
        return currentTurn == JANITOR;
    }

    void Update()
    {
        if (MapGenerator.currentTileTypes[Tile.NEUTRAL] <= 0)
        {
            if (MapGenerator.currentTileTypes[Tile.JANITOR] > MapGenerator.currentTileTypes[Tile.DEMON]) {
                endGame(DEMON);
                
            }
            else
            {
                endGame(JANITOR);
            }
        }
    }

    public void performAction()
    {
        turnsLeft--;
        if (turnsLeft <= 0)
        {
            changeTurns();
        }
    }

    void intializeMinionSetUp()
    {
    }

    public void endGame(int idLoser)
    {
        if (idLoser == JANITOR)
        {
            uiManager.GameEnds(DEMON);
        }
        uiManager.GameEnds(JANITOR);
    }

   

    void changeTurns()
    {
        turnsLeft = turnsPerPlayer;
        if (currentTurn == JANITOR)
        {
            currentTurn = DEMON;
        }
        else
        {
            currentTurn = JANITOR;
        }
        cameraManager.shiftCamera(currentTurn);
    }
}
