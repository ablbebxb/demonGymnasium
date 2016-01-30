using UnityEngine;
using System.Collections;

public class CleanTile : MonoBehaviour {
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void cleanTile()
    {
        anim.SetTrigger("CleanTile");
    }
}
