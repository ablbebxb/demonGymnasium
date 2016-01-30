using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {
    Tile[][] gridArray;



	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Tile getTileAtPosition(int i, int j)
    {
        if (i < 0 || i >= gridArray.GetLength(0) || j < 0 || j > gridArray.GetLength(1))
        {
            return null;
        }

        return gridArray[i][j];
    }
}
