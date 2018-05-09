using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	// Use this for initialization
	public float damageValue = 15f;

	void Start () {
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy"){
			other.gameObject.SendMessage ("Hit", damageValue);
		}

	}
	void Hit(float damageValue){
	}
}
