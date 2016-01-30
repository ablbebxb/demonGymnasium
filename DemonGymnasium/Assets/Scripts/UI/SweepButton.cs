using UnityEngine;
using System.Collections;

public class SweepButton : Button {

    void OnMouseDown()
    {
        this.GetComponentInParent<PlayerModal>().sweepButtonClicked();
    }

    new void OnMouseOver()
    {
        base.OnMouseOver();
        this.GetComponentInParent<PlayerModal>().sweepButtonHover();
    }

    new void OnMouseExit()
    {
        base.OnMouseExit();
        this.GetComponentInParent<PlayerModal>().sweepButtonExit();
    }
}
