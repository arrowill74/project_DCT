/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;
using System.Collections;
using System;

public class RWVR_InteractionController : MonoBehaviour
{
    public Transform snapColliderOrigin;
    public GameObject ControllerModel;

    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector3 angularVelocity;

    private RWVR_InteractionObject objectBeingInteractedWith;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public RWVR_InteractionObject InteractionObject
    {
        get { return objectBeingInteractedWith; }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void CheckForInteractionObject()
    {
        Collider[] overlappedColliders = Physics.OverlapSphere(snapColliderOrigin.position, snapColliderOrigin.lossyScale.x / 2f);

        foreach (Collider overlappedCollider in overlappedColliders)
        {
            if (overlappedCollider.CompareTag("InteractionObject") && overlappedCollider.GetComponent<RWVR_InteractionObject>().IsFree())
            {
                objectBeingInteractedWith = overlappedCollider.GetComponent<RWVR_InteractionObject>();
                objectBeingInteractedWith.OnTriggerWasPressed(this);
                return;
            }
        }
    }

    void Update()
    {
        if (Controller.GetHairTriggerDown())
        {
            CheckForInteractionObject();
        }

        if (Controller.GetHairTrigger())
        {
            if (objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnTriggerIsBeingPressed(this);
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectBeingInteractedWith)
            {
                objectBeingInteractedWith.OnTriggerWasReleased(this);
                objectBeingInteractedWith = null;
            }
        }
    }

    private void UpdateVelocity()
    {
        velocity = Controller.velocity;
        angularVelocity = Controller.angularVelocity;
    }

    void FixedUpdate()
    {
        UpdateVelocity();
    }

    public void HideControllerModel()
    {
        ControllerModel.SetActive(false);
    }

    public void ShowControllerModel()
    {
        ControllerModel.SetActive(true);
    }

    public void Vibrate(ushort strength)
    {
        Controller.TriggerHapticPulse(strength);
    }

    public void SwitchInteractionObjectTo(RWVR_InteractionObject interactionObject)
    {
        objectBeingInteractedWith = interactionObject;
        objectBeingInteractedWith.OnTriggerWasPressed(this);
    }
}
