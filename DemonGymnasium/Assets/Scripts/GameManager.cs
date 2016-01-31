using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public const int JANITOR = 0;
    public const int DEMON = 1;

    public int currentTurn;
    CameraManager cameraManager;

    void Start()
    {
        currentTurn = JANITOR;
        cameraManager = GetComponent<CameraManager>();
    }

    void Update()
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
