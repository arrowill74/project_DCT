using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerclosed : MonoBehaviour {

	// Use this for initialization
	public GameObject sand;
	public CollisionListScript PlayerSensor;
	private Rigidbody rigidBody;
	void Start () {
		rigidBody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerSensor.CollisionObjects.Count > 0) {
			sand.SetActive(false);
		}
	}
}
