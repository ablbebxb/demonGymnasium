using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class EndPanel_UI : MonoBehaviour {





	//0 means demon; 1 means Janitor
	public void Setup(int winner){
		if (winner == 0) {

		} 
		else if (winner == 1) {

		} 
		else {
			Debug.Log ("Unknown winner");
		}



	}


	public void MainMenuBtnClicked(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void RematchBtnClicked(){
		SceneManager.LoadScene ("MainScene");
	}
}
