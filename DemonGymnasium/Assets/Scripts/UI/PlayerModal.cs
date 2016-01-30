using UnityEngine;
using System.Collections;

public class PlayerModal : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void attackButtonClicked()
    {
        Debug.Log("open");
        GetComponentInChildren<Canvas>().gameObject.SetActive(true);//a hack, will not work with multiple sub-modals
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
