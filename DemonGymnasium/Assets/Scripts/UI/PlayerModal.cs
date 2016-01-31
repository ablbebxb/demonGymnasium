using UnityEngine;
using System.Collections;

public class PlayerModal : MonoBehaviour
{
    public void closeButtonClicked()
    {
        this.gameObject.SetActive(false);
    }

    public void moveButtonClicked()
    {
        GameManager.manager.setupMove();
    }

    public void shootButtonClicked()
    {
        GameManager.manager.setupAttack();
    }

    public void expandButtonClicked()
    {
        GameManager.manager.expand();
    }

}
