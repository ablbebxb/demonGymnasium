using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayBtnClicked(){
		SceneManager.LoadScene ("MainScene");
	}

	public void CreditBtnClicked(){
		
	}

	public void QuitBtnClicked(){
		Application.Quit ();
	}

}
