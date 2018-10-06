using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ColliderEvent : MonoBehaviour
{
    enum State
    {
        None,
        HoldingPart,
        HoldingInstrument
    }

    Hand handController;
    State state = State.None;

    bool isValid
    {
        get
        {
            return handController.controller != null;
        }
    }

    void Start()
    {
        handController = GetComponent<Hand>();
    }

    void Update()
    {
        if (isValid && handController.controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            if (state == State.HoldingPart && GetComponent<SpringJoint>())
            {
                SpringJoint fx = GetComponent<SpringJoint>();
                fx.connectedBody.velocity = -handController.controller.velocity;
                fx.connectedBody.angularVelocity = -handController.controller.angularVelocity;
                fx.connectedBody = null;
                Destroy(fx);
                state = State.None;
                Debug.Log("HoldingPart -> None");
                needHaptic = true; //
            }

            if (state == State.HoldingInstrument && GetComponent<FixedJoint>())
            {
                FixedJoint fx = gameObject.GetComponent<FixedJoint>();
                fx.connectedBody.velocity = -handController.controller.velocity;
                fx.connectedBody.angularVelocity = -handController.controller.angularVelocity;
                fx.connectedBody = null;
                Destroy(fx);
                state = State.None;
                Debug.Log("HoldingInstrument -> None");
                needHaptic = true; //
            }
        }
    }

    private bool needHaptic = true;

    void OnTriggerStay(Collider other)
    {
        if (!isValid)
        {
            return;
        }

        if (needHaptic == true)
            handController.controller.TriggerHapticPulse(700);

        if (handController.controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            SpringJoint springJoint = GetComponent<SpringJoint>();
            FixedJoint fixedJoint = GetComponent<FixedJoint>();

            if (state == State.None && other.gameObject.layer == LayerMask.NameToLayer("Parts"))
            {
                SpringJoint fx = gameObject.AddComponent<SpringJoint>();
                fx.spring = 600.0f;
                fx.damper = 100.0f;
                fx.connectedBody = other.attachedRigidbody;
                state = State.HoldingPart;
                Debug.Log("HoldingPart");
                needHaptic = false; //
            }
            else if (state == State.None && other.gameObject.layer == LayerMask.NameToLayer("Instruments"))
            {
                FixedJoint fx = gameObject.AddComponent<FixedJoint>();
                fx.connectedBody = other.attachedRigidbody;
                state = State.HoldingInstrument;
                Debug.Log("HoldingInstrument");
                needHaptic = false; //
            }
        }
    }
}
