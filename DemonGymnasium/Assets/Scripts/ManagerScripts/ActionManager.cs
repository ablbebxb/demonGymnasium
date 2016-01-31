using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour {
    public const int MOVING = 0;
    public const int SHOOT = 1;
    public const int EXPAND = 2;

    int currentAction;
    
    void Start()
    {
        currentAction = -1;
    }


}
