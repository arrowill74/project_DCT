using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_test : MonoBehaviour {
	public float speed = 1f;
	Vector3 movement, moveDir;
	Animator anim;
	Rigidbody spiderRigidbody;
	float xDir;
	float yDir;
	float zDir;
	void Awake(){
		//		Tomb = GameObject.FindGameObjectWithTag ("tomb").transform;
		//		nav = GetComponent<UnityEngine.AI.NavMeshAgent> ();

		anim = GetComponent<Animator> ();
		spiderRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (Time.frameCount % 100 == 1) {
			xDir = 1f;
//			Random.Range (-1.0f, 1.0f);
			yDir = 0f;
//				Random.Range (-1.0f, 1.0f);
			zDir = 0f;
//			Random.Range (-1.0f, 1.0f);
			moveDir.Set (xDir, yDir, zDir);
			Rotat (moveDir, zDir);
		}
		Move (xDir, yDir, zDir);
		Animating (yDir, zDir);
	}

	void Move(float xDir, float yDir, float zDir){
		movement.Set (xDir, yDir, zDir);
		movement = movement.normalized *speed* Time.deltaTime*0.1f;
		spiderRigidbody.MovePosition (transform.position + movement);
	}
	void Rotat(Vector3 dir, float z){
		transform.rotation = Quaternion.LookRotation (moveDir);
		if (z < 0) {
			transform.Rotate (new Vector3 (0f, 0f, -90f));
		} else {
			transform.Rotate (new Vector3 (0f, 0f, 90f));
		}

	}
	void Animating(float yDir, float zDir){
		bool walking = yDir != 0f || zDir != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
