using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	/*
	 * Default settings
	 */
	public int startingActionPoints;


	private int actionPoints;

	// Use this for initialization
	void Start () {
		actionPoints = startingActionPoints;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
