using UnityEngine;
using System.Collections;

public class MoveButton : Button {

    void OnMouseDown()
    {
        this.GetComponentInParent<PlayerModal>().moveButtonClicked();
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
