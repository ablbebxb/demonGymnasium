using UnityEngine;
using System.Collections;

public class AttackButton : Button
{

	void OnMouseDown()
    {
        Debug.Log("Click");
        this.GetComponentInParent<PlayerModal>().attackButtonClicked();
    }

    /*new void OnMouseOver()
    {
        base.OnMouseOver();
    }

    new void OnMouseExit()
    {
        base.OnMouseExit();
    }*/
}
