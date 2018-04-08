using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour {
	
	public float speed = 1f;
	Vector3 movement, moveDir;
	Animator anim;
	Rigidbody spiderRigidbody;
	float xDir;
	float yDir;
	float zDir;
//	Transform Tomb;
//	UnityEngine.AI.NavMeshAgent nav;
	void Awake(){
//		Tomb = GameObject.FindGameObjectWithTag ("tomb").transform;
//		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();

		anim = GetComponent<Animator> ();
		spiderRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (Time.frameCount % 100 == 1) {
			xDir = 0f;
			yDir = Random.Range (-1.0f, 1.0f);
			zDir = Random.Range (-1.0f, 1.0f);
			moveDir.Set (xDir, yDir, zDir);
			Rotat (moveDir);
		}
		Move (xDir, yDir, zDir);
		Animating (yDir, zDir);
	}
		
	void Move(float xDir, float yDir, float zDir){
		movement.Set (xDir, yDir, zDir);
		movement = movement.normalized *speed* Time.deltaTime*0.1f;
		spiderRigidbody.MovePosition (transform.position + movement);
	}

	void Rotat(Vector3 dir){
		transform.rotation = Quaternion.LookRotation (moveDir);
		transform.Rotate (new Vector3 (0f, 0f, 90f));
	}

	void Animating(float yDir, float zDir){
		bool walking = yDir != 0f || zDir != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
