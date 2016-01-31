using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;

    public int turnsPerPlayer;

    public int currentTurn;
    public LinkedList<Entity> currentPlayers;
    public bool gameInProgress = true;
    int turnsLeft;
    int winner = -1;
    CameraManager cameraManager;
    UIManager uiManager;


    void Start()
    {
        currentPlayers = new LinkedList<Entity>();
        currentTurn = JANITOR;
        cameraManager = GetComponent<CameraManager>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        turnsLeft = turnsPerPlayer;
    }

    void Update()
    {

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

    public void endGame(int id)
    {
<<<<<<< HEAD
        if (id == JANITOR)
        {
            uiManager.GameEnds(DEMON);
        }
        uiManager.GameEnds(JANITOR);
    }

    public void endGameBoardFull()
    {

=======
        uiManager.GameEnds(1);
>>>>>>> f78a3dcea0213915d1d3740431f40cf8664929f3
    }

    void changeTurns()
    {
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
