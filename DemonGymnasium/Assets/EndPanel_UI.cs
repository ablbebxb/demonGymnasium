using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class EndPanel_UI : MonoBehaviour {
	
	public void MainMenuBtnClicked(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void RematchBtnClicked(){
		SceneManager.LoadScene ("MainScene");
	}
}
