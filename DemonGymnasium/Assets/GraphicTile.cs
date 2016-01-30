using UnityEngine;
using System.Collections;

public class GraphicTile : MonoBehaviour {
    public Texture[] texutureTypes;
    string[] triggerNames = { "Janitor", "Demon", "Neutral"};
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    public void selectTileTupe(int tileType)
    {
        foreach(string n in triggerNames)
        {
            anim.ResetTrigger(n);
        }
        anim.SetTrigger(triggerNames[tileType]);
    }

    
}
