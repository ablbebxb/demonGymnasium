using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public const int PLAYER_ONE = 0; //Janitors
    public const int PLAYER_TWO = 1; //Demons
    int currentPlayer;
    MapGenerator mapGenerator;

    void Start()
    {
        mapGenerator = GameObject.FindObjectOfType<MapGenerator>();
    }


}
