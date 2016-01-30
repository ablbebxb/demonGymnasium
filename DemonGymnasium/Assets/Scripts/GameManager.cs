using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager manager;

	private bool isHumanTurn;//true- human, false- monster
	private Entity selectedObject;//the currently selected player/monster(/obstacle)
	private Transform mainCameraTransform;

	// Use this for initialization
	void Start () {
		GameManager.manager = this;
		isHumanTurn = true;
		mainCameraTransform = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void selectPlayer(Player player) {
		if (isHumanTurn) {
			selectedObject = player;
		}
	}

	public void selectMonster(Monster monster) {
		if (!isHumanTurn) {
			selectedObject = monster;
		}
	}

	public void selectTile(Tile tile) {

	}

	public Transform getCamera() {
		return mainCameraTransform;
	}
}
