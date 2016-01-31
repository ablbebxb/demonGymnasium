using UnityEngine;
using System.Collections;

public class PlayerModal : MonoBehaviour
{
	public float OffsetX;
	public float OffsetY;
	public bool enabled;

	private GameObject actionPanel;
	private GameObject subAttackPanel;
	private GameObject shootButton;
	private GameObject expandButton;


	private bool outOfSubAttackPanel;
	private bool outOfAttackButton;
	private bool outOfActionPanel;
	private bool enteredActionPanel;

	public void Awake(){
		actionPanel = GetComponent<RectTransform>().Find ("ActionPanel").gameObject;
		subAttackPanel = actionPanel.GetComponent<RectTransform>().Find ("SubAttackPanel").gameObject;
		shootButton = subAttackPanel.GetComponent<RectTransform>().Find ("ShootButton").gameObject;
		expandButton = subAttackPanel.GetComponent<RectTransform>().Find ("ExpandButton").gameObject;
	}

	public void Start(){
		enteredActionPanel = false;
		outOfSubAttackPanel = true;
		outOfAttackButton = true;
		outOfActionPanel = true;
	}

	public void Reset(){
		enabled = true;
		enteredActionPanel = false;
		outOfSubAttackPanel = true;
		outOfAttackButton = true;
		outOfActionPanel = true;
	}

	public void OnGUI(){
		CheckSubAttackPanel();
		CheckActionPanel ();
	}

	public void Enable(){
		Reset ();
		actionPanel.SetActive (true);
	}

	public void Disable(){
		enabled = false;
		actionPanel.SetActive (false);
		DisableSubAttackPanel ();
	}



	void CheckActionPanel(){
		if(outOfActionPanel && Input.GetMouseButtonDown(0) && enteredActionPanel){
			enteredActionPanel = false;
			Disable ();
		}
	}

	void CheckSubAttackPanel(){
		if(outOfSubAttackPanel && outOfAttackButton){
			Debug.Log ("Disabling subattack panel");

			DisableSubAttackPanel ();
		}
	}

	public void SetUIPos(Transform entityTrans){
		RectTransform actionRt = actionPanel.GetComponent<RectTransform> ();

		Vector3 viewPos = Camera.main.WorldToViewportPoint (entityTrans.position);
		viewPos = new Vector3 (viewPos.x, viewPos.y);
		actionRt.anchorMin = viewPos;
		actionRt.anchorMax = viewPos;
		actionRt.anchoredPosition = new Vector2 (OffsetX, OffsetY);
	}

	public void ActionPanelHovered(){
		enteredActionPanel = true;
		outOfActionPanel = false;


	}

	public void ActionPanelExited(){
		if(enteredActionPanel){
			outOfActionPanel = true;
		}

	}

	public void AttackButtonHovered(){
		outOfAttackButton = false;
		subAttackPanel.SetActive (true);
		shootButton.SetActive (true);
		expandButton.SetActive (true);

	}

	public void AttackButtonExited(){
		if(enteredActionPanel){
			outOfAttackButton = true;
		}

	}

	public void subAttackPanelHovered (){
		if(enteredActionPanel){
			outOfSubAttackPanel = false;
		}

	}

	public void subAttackPanelExited (){
		if(enteredActionPanel){
			outOfSubAttackPanel = true;
		}

	} 

	public void DisableSubAttackPanel(){
		if(enteredActionPanel){
			shootButton.SetActive (false);
			expandButton.SetActive (false);
		}

	}

    public void moveButtonClicked()
    {
        GameManager.manager.setupMove();
		Disable ();
    }

    public void shootButtonClicked()
    {
        GameManager.manager.setupAttack();
		Disable ();
    }

    public void expandButtonClicked()
    {
        GameManager.manager.expand();
		Disable ();
    }

}
