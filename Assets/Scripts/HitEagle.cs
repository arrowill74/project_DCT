using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEagle : MonoBehaviour {
	public float damageValue = 15f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy"){
			other.gameObject.SendMessage ("Hit", damageValue);
		}
	}
	void Hit(float damageValue){
	}
}
