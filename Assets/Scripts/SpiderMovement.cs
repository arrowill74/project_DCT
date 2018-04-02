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

	void Awake(){
		anim = GetComponent<Animator> ();
		spiderRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		if (Time.frameCount % 100 == 1) {
			yDir = Random.Range (-1.0f, 1.0f);
			zDir = Random.Range (-1.0f, 1.0f);
			xDir = Mathf.Sqrt(Mathf.Pow(yDir,2f)+Mathf.Pow(zDir,2f));
			moveDir.Set (xDir, 0f, 0f);
//			Rotat (moveDir);
			if (Physics.Raycast (transform.position, transform.up)) {
				moveDir = chooseDirection ();
				transform.rotation = Quaternion.LookRotation (moveDir);
			}
		}

		Move (xDir,yDir, zDir);

		Animating (yDir, zDir);
		Debug.Log (movement);
		Debug.Log (moveDir);
//		spiderRigidbody.velocity = moveDir * speed;
	}
		
	void Move(float xDir,float yDir, float zDir){
		movement.Set (0f, yDir, zDir);
		movement = movement.normalized *speed* Time.deltaTime*0.1f;
		spiderRigidbody.MovePosition (transform.position + movement);
		//moveDirection = transform.TransformDirection(movement);
		//spiderRigidbody.transform.Rotate(Time.deltaTime*100f, 0, 0,Space.World);.

	}

	void Rotat(Vector3 dir){
//		dir = dir.normalized *speed* Time.deltaTime*0.1f;
		transform.Rotate (dir, Space.World);
	}

	void Animating(float yDir, float zDir){
		bool walking = yDir != 0f || zDir != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
