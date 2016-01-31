using UnityEngine;
using System.Collections;

public class AbilityFX : MonoBehaviour {
	
	private Animator animator;
	private SpriteRenderer rend_base;
	private SpriteRenderer rend_particle;

	public virtual void Awake(){
		animator = GetComponent<Animator> ();
		rend_base = GetComponent<SpriteRenderer> ();
		rend_particle = transform.Find ("Particles").gameObject.GetComponent<SpriteRenderer>();
	}

	public virtual void Setup (Transform targetTransform){



		Vector3 targetPos = targetTransform.position;
		transform.position = targetPos;
	}

	public virtual void Play (){}


	public virtual void Enable(){


	}
	public virtual void Remove (){}



}
