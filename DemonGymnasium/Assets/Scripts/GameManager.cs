using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private int player;
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;

	// Use this for initialization
	void Start () {
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setSelectedObject(Entity obj) {
		selectedObject = obj;
	}

	public Transform getCamera() {
		return mainCameraTransform;
	}
}
