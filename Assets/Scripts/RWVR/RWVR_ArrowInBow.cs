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

public class RWVR_ArrowInBow : RWVR_InteractionObject
{
    public float minimumPosition;
    public float maximumPosition;

    private Transform attachedBow;
    private const float arrowCorrection = 0.3f;

    public override void Awake()
    {
        base.Awake();
        attachedBow = transform.parent;
    }

    public override void OnTriggerIsBeingPressed(RWVR_InteractionController controller)
    {
        base.OnTriggerIsBeingPressed(controller);

        Vector3 arrowInBowSpace = attachedBow.InverseTransformPoint(controller.transform.position);
        cachedTransform.localPosition = new Vector3(0, 0, arrowInBowSpace.z + arrowCorrection);
    }

    public override void OnTriggerWasReleased(RWVR_InteractionController controller)
    {
        attachedBow.GetComponent<Bow>().ShootArrow();
        currentController.Vibrate(3500);
        base.OnTriggerWasReleased(controller);
    }

    void LateUpdate()
    {
        // Limit position
        float zPos = cachedTransform.localPosition.z;
        zPos = Mathf.Clamp(zPos, minimumPosition, maximumPosition);
        cachedTransform.localPosition = new Vector3(0, 0, zPos);

        //Limit rotation
        cachedTransform.localRotation = Quaternion.Euler(Vector3.zero);

        if (currentController)
        {
            currentController.Vibrate(Convert.ToUInt16(500 * -zPos));
        }
    }
}
