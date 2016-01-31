using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public enum UISTATE
	{
		Ingame,
		Endgame
	}
	public UISTATE currentState;


	private GameObject actionPanel;
	private GameObject endPanel;

	//only reset the action panel once after the in game state is entered
	private bool ingame_reset;

	void Awake(){
		actionPanel = ((RectTransform)transform).Find ("ActionPanel").gameObject;
		endPanel = ((RectTransform)transform).Find ("EndPanel").gameObject;
	}

	// Use this for initialization
	void Start () {
		ingame_reset = false;
		currentState = UISTATE.Ingame;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		switch(currentState){

			case UISTATE.Ingame:
				StartIngame ();

				break;
			

			case UISTATE.Endgame:
				StartEndgame ();

				break;
		}
	}

	void StartIngame(){
		if(!ingame_reset){
			actionPanel.SetActive (false);
			ingame_reset = true;
		}

		endPanel.SetActive (false);
	}

	void StartEndgame(){
		ingame_reset = false;
		actionPanel.SetActive (false);
		endPanel.SetActive (true);
	}
}
