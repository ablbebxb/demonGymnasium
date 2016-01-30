using UnityEngine;
using System.Collections;

public class AttackButton : Button
{

	void OnMouseDown()
    {
        this.GetComponentInParent<PlayerModal>().attackButtonClicked();
    }
}
