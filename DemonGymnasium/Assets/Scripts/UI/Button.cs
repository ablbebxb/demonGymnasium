using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour {

	public void OnMouseOver()
    {
        GetComponent<Image>().color = Color.gray;
    }

    public void OnMouseExit()
    {
        GetComponent<Image>().color = Color.black;
    }
}
