using UnityEngine;
using System.Collections;

public class PlayerModal : MonoBehaviour
{

    public GameObject[] subModals;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void attackButtonClicked()
    {
        Debug.Log("open");
        subModals[0].SetActive(true);
    }

    public void moveButtonClicked()
    {
        //TODO enter move mode
    }

    public void shootButtonClicked()
    {
        //TODO enter shoot mode
    }

    public void shootButtonHover()
    {
        //TODO highlight potential enemy targets
    }

    public void shootButtonExit()
    {
        //TODO highlight potential enemy targets
    }

    public void sweepButtonClicked()
    {
        //TODO sweep
    }

    public void sweepButtonHover()
    {
        //TODO highlight targeted tiles for sweep
    }

    public void sweepButtonExit()
    {
        //TODO highlight potential enemy targets
    }
}
