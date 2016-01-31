using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    public Text janitorText;
    public Text demonText;

    void Update()
    {
        int janitorScore = MapGenerator.currentTileTypes[Tile.JANITOR];
        janitorText.text = "Janitors: " + janitorScore;
        demonText.text = "Demons: " + MapGenerator.currentTileTypes[Tile.DEMON];

    }
}
