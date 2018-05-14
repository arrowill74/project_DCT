using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpiderDeath : MonoBehaviour {
	private Animator animator;
	private float MinimumHitPeriod = 1f;
	private float HitCounter = 0;
	public float CurrentHP = 10;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		explosion.SetActive (false);
	}

	public void Hit(float value){
		if (HitCounter <= 0) {
			HitCounter = MinimumHitPeriod;
			CurrentHP -= value;

			animator.SetFloat ("HP",CurrentHP);
			animator.SetTrigger ("Hit");
			explosion.SetActive (true);
			if (CurrentHP <= 0) {BuryTheBody ();}
		}
	}

	void BuryTheBody(){
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Collider> ().enabled = false;
		this.transform.DOMoveY (-0.8f, 1f).SetRelative(true).SetDelay(0.5f).OnComplete (()=>
			{
				this.transform.DOMoveY (-0.8f, 1f).SetRelative(true).SetDelay(0).OnComplete(()=>
					{
						GameObject.Destroy(this.gameObject);
					});
			});
	}

	// Update is called once per frame
	void Update () {
		if (CurrentHP>0&&HitCounter > 0) {
			HitCounter -= Time.deltaTime;
		}
	}
}
