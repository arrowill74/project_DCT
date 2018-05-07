using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleMovement : MonoBehaviour {

	// Use this for initialization
	public float MoveSpeed;
	public GameObject FollowTarget;
	private Rigidbody rigidBody;
	public float Speed = 7;

	private Animator animator;
	void Start () {
		animator = this.GetComponent<Animator> ();
		rigidBody = this.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookAt = FollowTarget.gameObject.transform.position;

		this.transform.LookAt (lookAt);
		animator.SetBool("Armature|Fly", true);

		this.transform.position = Vector3.MoveTowards(this.transform.position, lookAt, Speed * Time.deltaTime);

	}
}
