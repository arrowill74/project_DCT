using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToController : InteractionObject {

	public bool hideControllerModel; // 1
	public Vector3 snapPositionOffset; // 2
	public Vector3 snapRotationOffset; // 3

	private Rigidbody rb; // 4

	public override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody>();
	}
	private void ConnectToController(InteractionController controller) // 1
	{
		cachedTransform.SetParent(controller.transform); // 2

		cachedTransform.rotation = controller.transform.rotation; // 3
		cachedTransform.Rotate(snapRotationOffset);
		cachedTransform.localScale= new Vector3 (1, 1, 1);
		cachedTransform.position = controller.snapColliderOrigin.position; // 4
		cachedTransform.Translate(snapPositionOffset, Space.Self);

		rb.useGravity = false; // 5
		rb.isKinematic = true; // 6
	}
	private void ReleaseFromController(InteractionController controller) // 1
	{
		cachedTransform.SetParent(null); // 2
		cachedTransform.localScale= new Vector3 (1, 1, 1);
		rb.useGravity = true; // 3
		rb.isKinematic = false;

		rb.velocity = controller.velocity; // 4
		rb.angularVelocity = controller.angularVelocity;
	}
	public override void OnTriggerWasPressed(InteractionController controller) // 1
	{
		base.OnTriggerWasPressed(controller); // 2

		if (hideControllerModel) // 3
		{
			controller.HideControllerModel();

		}

		ConnectToController(controller); // 4
	}
	public override void OnTriggerWasReleased(InteractionController controller) // 1
	{
		base.OnTriggerWasReleased(controller); // 2

		if (hideControllerModel) // 3
		{
			controller.ShowControllerModel();
		}

		ReleaseFromController(controller); // 4
	}

}
