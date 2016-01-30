using UnityEngine;
using System.Collections;

public class ShootButton : Button
{

    void OnMouseDown()
    {
        this.GetComponentInParent<PlayerModal>().shootButtonClicked();
    }

    new void OnMouseOver()
    {
        base.OnMouseOver();
        this.GetComponentInParent<PlayerModal>().shootButtonHover();
    }

    new void OnMouseExit()
    {
        base.OnMouseExit();
        this.GetComponentInParent<PlayerModal>().shootButtonExit();
    }

}
