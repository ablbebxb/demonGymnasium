using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;

    public int currentTurn;
    public LinkedList<Entity> currentPlayers;

    public GameObject janitorMinion;
    public GameObject janitorHero;
    public GameObject demonMinion;
    public GameObject demonHero;

    CameraManager cameraManager;

    void Start()
    {
        currentPlayers = new LinkedList<Entity>();
        currentTurn = JANITOR;
        cameraManager = GetComponent<CameraManager>();
    }

    void Update()
    {

    }

    void intializeMinionSetUp()
    {
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
