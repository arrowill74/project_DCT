using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBurnSpider : MonoBehaviour {

	// Use this for initialization

	void Start () {
		Rigidbody rigidbody = this.GetComponent<Rigidbody> ();

	}
	public float damageValue = 15;
	void OnTriggerEnter(Collider other) 
	{
		other.gameObject.SendMessage ("Hit",damageValue);

		//
	}
}
