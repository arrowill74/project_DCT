﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Monster:MonoBehaviour{
	private Animator animator;
	private float MinimumHitPeriod = 1f;
	private float HitCounter = 0;
	public float CurrentHP = 100;
	public GameObject organ;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
	}
	public void Hit(float value){
		if (HitCounter <= 0) {
			HitCounter = MinimumHitPeriod;
			CurrentHP -= value;

			animator.SetFloat ("HP",CurrentHP);
			animator.SetTrigger ("Hit");

			if (CurrentHP <= 0) {
				BuryTheBody ();
				pos = transform.position;

			}
		}
	}
	void BuryTheBody(){
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Collider> ().enabled = false;
		this.transform.DOMoveY (0f, 0f).SetRelative(true).SetDelay(0).OnComplete (()=>
		{
				this.transform.DOMoveY (-0.8f, 0.5f).SetRelative(true).SetDelay(0).OnComplete(()=>
				{
						GameObject.Destroy(this.gameObject);
						GameObject newOrgan = GameObject.Instantiate (organ);
						newOrgan.transform.position = pos;
						newOrgan.transform.DOMoveY (-0.8f, 0f).SetRelative(true).SetDelay(1).OnComplete(()=>
						{
							GameObject.Destroy(newOrgan.gameObject);
						});
				});
		});
	}

	void Update () {
		if (CurrentHP>0&&HitCounter > 0) {
			HitCounter -= Time.deltaTime;
		}
	}
} 
