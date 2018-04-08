using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killitself : MonoBehaviour {

	// Use this for initialization
	float aliveTime = 1;
	void Start () {
		Invoke ("DestroySelf", aliveTime);
	}
	
	// Update is called once per frame
	public void DestroySelf(){
		GameObject.Destroy (this.gameObject);
	}
}
